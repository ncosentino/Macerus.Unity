using System;
using System.Linq;

using Assets.Scripts.Plugins.Features.Hud.Api;
using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;

using Macerus.Api.Behaviors;
using Macerus.Game.GameObjects;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class InventoryItemDropUiFlow : IInventoryItemDropUiFlow
    {
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IDropItemHandler _dropItemHandler;
        private readonly ISocketPatternHandlerFacade _socketPatternHandler;
        private readonly ISocketableInfoFactory _socketableInfoFactory;

        public InventoryItemDropUiFlow(
            IGameObjectManager gameObjectManager,
            IDropItemHandler dropItemHandler,
            ISocketPatternHandlerFacade socketPatternHandler,
            ISocketableInfoFactory socketableInfoFactory)
        {
            _gameObjectManager = gameObjectManager;
            _dropItemHandler = dropItemHandler;
            _socketPatternHandler = socketPatternHandler;
            _socketableInfoFactory = socketableInfoFactory;
        }

        public void Execute(DroppedEventArgs droppedEventArgs)
        {
            if (droppedEventArgs.DroppedOnto == null)
            {
                TryDropItem(droppedEventArgs.Dropped);
                return;
            }

            if (TrySocketItem(
                droppedEventArgs.Dropped,
                droppedEventArgs.DroppedOnto))
            {
                return;
            }

            // put other handlers here...
        }

        private bool TrySocketItem(
            GameObject dropped,
            GameObject droppedOnto)
        {
            var sourceInventoryItemBehaviour = dropped.GetComponentInParent<IInventoryItemBehaviour>();
            if (sourceInventoryItemBehaviour == null)
            {
                return false;
            }

            var destinationInventoryItemBehaviour = droppedOnto.GetComponentInParent<IInventoryItemBehaviour>();
            if (destinationInventoryItemBehaviour == null)
            {
                return false;
            }

            var canFitSocketBehavior = dropped.FirstOrDefault<ICanFitSocketBehavior>();
            if (canFitSocketBehavior == null)
            {
                return false;
            }

            if (!destinationInventoryItemBehaviour
                .InventoryItem
                .TryGetFirst<ICanBeSocketedBehavior>(out var canBeSocketedBehavior))
            {
                return false;
            }

            if (canBeSocketedBehavior.Owner == canFitSocketBehavior.Owner)
            {
                return false;
            }

            if (!canBeSocketedBehavior.CanFitSocket(canFitSocketBehavior))
            {
                return false;
            }

            if (!canBeSocketedBehavior.Socket(canFitSocketBehavior))
            {
                throw new InvalidOperationException(
                    $"Check to socket '{canFitSocketBehavior}' into " +
                    $"'{canBeSocketedBehavior}' passed, but " +
                    $"{nameof(ICanBeSocketedBehavior.Socket)}() was not " +
                    $"successful.");
            }

            if (!sourceInventoryItemBehaviour.TryRemoveFromSourceContainer())
            {
                throw new InvalidOperationException(
                    $"'{canFitSocketBehavior}' was socketed into " +
                    $"'{canBeSocketedBehavior}', but could not remove the item " +
                    $"from the source container.");
            }

            if (_socketPatternHandler.TryHandle(
                _socketableInfoFactory.Create(
                    (IGameObject)canBeSocketedBehavior.Owner,
                    canBeSocketedBehavior),
                out var newSocketPatternItem))
            {
                if (!destinationInventoryItemBehaviour.TryRemoveFromSourceContainer())
                {
                    throw new InvalidOperationException(
                        $"A socket pattern item was created, but the source " +
                        $"item '{canBeSocketedBehavior.Owner}' could not be " +
                        $"removed from '{destinationInventoryItemBehaviour.SourceItemContainerCallback()}'. " +
                        $"Is it possible the two items being socketed together " +
                        $"were not from the same inventory?");
                }

                if (!destinationInventoryItemBehaviour
                     .SourceItemContainerCallback()
                     .TryAddItem(newSocketPatternItem))
                {
                    throw new InvalidOperationException(
                        $"A socket pattern item '{newSocketPatternItem}' was " +
                        $"created, but could not be added to " +
                        $"'{destinationInventoryItemBehaviour.SourceItemContainerCallback()}'.");
                }
            }

            return true;
        }

        private bool TryDropItem(GameObject dropped)
        {
            var inventoryItemBehaviour = dropped.GetRequiredComponent<IInventoryItemBehaviour>();
            var playerLocation = _gameObjectManager
                .GetPlayer()
                .GetOnly<IReadOnlyWorldLocationBehavior>();

            return _dropItemHandler.TryDropItem(
                playerLocation.X,
                playerLocation.Y,
                inventoryItemBehaviour.InventoryItem,
                () =>
                {
                    if (!inventoryItemBehaviour.TryRemoveFromSourceContainer())
                    {
                        Debug.Log(
                            $"Could not remove '{inventoryItemBehaviour.InventoryItem}' " +
                            $"from its container.");
                        return false;
                    }

                    return true;
                });
        }
    }
}