using Assets.Scripts.Plugins.Features.Hud.Api;
using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;
using Assets.Scripts.Unity.Resources.Prefabs;

using Macerus.Api.Behaviors;
using Macerus.Game.GameObjects;

using ProjectXyz.Api.GameObjects;
using NexusLabs.Contracts;

using UnityEngine;
using UnityEngine.EventSystems;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using System.Linq;
using System;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public class DragInventoryListItemBehaviour :
        MonoBehaviour,
        IDragInventoryListItemBehaviour,
        IDragHandler,
        IEndDragHandler
    {
        private IDragItemPrefab _dragObject;

        public GameObject InventoryGameObject { get; set; }

        public IDragItemFactory DragItemFactory { get; set; }

        public IObjectDestroyer ObjectDestroyer { get; set; }

        public IInventoryListItemPrefab InventoryListItem { get; set; }

        public IDropItemHandler DropItemHandler { get; set; }

        public IMouseInput MouseInput { get; set; }

        public IGameObjectManager GameObjectManager { get; set; }

        public ISocketPatternHandlerFacade SocketPatternHandler { get; set; }

        public ISocketableInfoFactory SocketableInfoFactory { get; set; }

        public void Start()
        {
            Contract.RequiresNotNull(
                InventoryGameObject,
                $"{nameof(InventoryGameObject)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                DragItemFactory,
                $"{nameof(DragItemFactory)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                ObjectDestroyer,
                $"{nameof(ObjectDestroyer)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                InventoryListItem,
                $"{nameof(InventoryListItem)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                DropItemHandler,
                $"{nameof(DropItemHandler)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                MouseInput,
                $"{nameof(MouseInput)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                GameObjectManager,
                $"{nameof(GameObjectManager)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                SocketPatternHandler,
                $"{nameof(SocketPatternHandler)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                SocketableInfoFactory,
                $"{nameof(SocketableInfoFactory)} was not set on '{gameObject}.{this}'.");
        }

        public void OnDestroy()
        {
            ObjectDestroyer.Destroy(_dragObject?.GameObject);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (_dragObject == null)
            {
                _dragObject = DragItemFactory.Create(InventoryListItem.Icon);
                _dragObject.SetParent(InventoryGameObject.transform);
            }

            _dragObject
                .GameObject
                .transform
                .position = MouseInput.Position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            try
            {
                var droppedOnCanvas = eventData
                    .pointerCurrentRaycast
                    .gameObject == null;
                if (droppedOnCanvas)
                {
                    TryDropItem(eventData);
                    return;
                }

                var canFitSocketBehavior = eventData
                    .pointerDrag
                    .FirstOrDefault<ICanFitSocketBehavior>();
                if (canFitSocketBehavior != null)
                {
                    var canBeSocketedBehavior = eventData
                        .pointerCurrentRaycast
                        .gameObject
                        ?.GetInThisOrUpHierarchy<ICanBeSocketedBehavior>()
                        .FirstOrDefault();
                    if (canBeSocketedBehavior != null)
                    {
                        TrySocketItem(
                            canBeSocketedBehavior,
                            canFitSocketBehavior,
                            eventData
                                .pointerDrag
                                .GetRequiredComponent<IInventoryItemBehaviour>());
                        return;
                    }
                }
            }
            finally
            {
                ObjectDestroyer.Destroy(_dragObject.GameObject);
                _dragObject = null;
            }
        }

        private bool TrySocketItem(
            ICanBeSocketedBehavior canBeSocketedBehavior,
            ICanFitSocketBehavior canFitSocketBehavior,
            // FIXME: I hate that this is a unity-based API here...
            IInventoryItemBehaviour inventoryItemBehaviour)
        {
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

            if (!inventoryItemBehaviour.TryRemoveFromSourceContainer())
            {
                throw new InvalidOperationException(
                    $"'{canFitSocketBehavior}' was socketed into " +
                    $"'{canBeSocketedBehavior}', but could not remove the item " +
                    $"from the source container.");
            }

            if (SocketPatternHandler.TryHandle(
                SocketableInfoFactory.Create(
                    (IGameObject)canBeSocketedBehavior.Owner,
                    canBeSocketedBehavior),
                out var newSocketPatternItem))
            {
                // FIXME: this assumes the item that was socketed is in the 
                // same inventory container as the item it was socketed into
                if (!inventoryItemBehaviour
                    .SourceItemContainerCallback()
                    .TryRemoveItem((IGameObject)canBeSocketedBehavior.Owner))
                {
                    throw new InvalidOperationException(
                        $"A socket pattern item was created, but the source " +
                        $"item '{canBeSocketedBehavior.Owner}' could not be " +
                        $"removed from '{inventoryItemBehaviour.SourceItemContainerCallback()}'. " +
                        $"Is it possible the two items being socketed together " +
                        $"were not from the same inventory?");
                }

                if (!inventoryItemBehaviour
                     .SourceItemContainerCallback()
                     .TryAddItem(newSocketPatternItem))
                {
                    throw new InvalidOperationException(
                        $"A socket pattern item '{newSocketPatternItem}' was " +
                        $"created, but could not be added to " +
                        $"'{inventoryItemBehaviour.SourceItemContainerCallback()}'.");
                }
            }

            return true;
        }

        private bool TryDropItem(PointerEventData eventData)
        {
            var inventoryItemBehaviour = eventData
                .pointerDrag
                .GetRequiredComponent<IInventoryItemBehaviour>();
            var playerLocation = GameObjectManager
                .GetPlayer()
                .GetOnly<IReadOnlyWorldLocationBehavior>();

            return DropItemHandler.TryDropItem(
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
