using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Plugins.Features.Hud.Api;
using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;
using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;

using Macerus.Api.Behaviors;
using Macerus.Game.GameObjects;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment
{
    public sealed class EquipmentItemDropUiFlow : IEquipmentItemDropUiFlow
    {
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;
        private readonly IDropItemHandler _dropItemHandler;

        public EquipmentItemDropUiFlow(
            IReadOnlyMapGameObjectManager mapGameObjectManager,
            IDropItemHandler dropItemHandler)
        {
            _mapGameObjectManager = mapGameObjectManager;
            _dropItemHandler = dropItemHandler;
        }

        public void Execute(DroppedEventArgs droppedEventArgs)
        {
            if (droppedEventArgs.DroppedOnto == null)
            {
                TryDropItem(droppedEventArgs.Dropped);
                return;
            }

            // put other handlers here...
        }

        private bool TryDropItem(GameObject dropped)
        {
            var dropEquipmentSlotBehaviour = dropped.GetRequiredComponent<IDropEquipmentSlotBehaviour>();
            var item = dropped
                .GetRequiredComponent<IReadOnlyHasGameObject>()
                ?.GameObject;
            var playerLocation = _mapGameObjectManager
                .GetPlayer()
                .GetOnly<IReadOnlyWorldLocationBehavior>();

            return _dropItemHandler.TryDropItem(
                playerLocation.X,
                playerLocation.Y,
                item,
                () =>
                {
                    if (!dropEquipmentSlotBehaviour
                        .CanEquipBehavior
                        .TryUnequip(
                            dropEquipmentSlotBehaviour.TargetEquipSlotId,
                            out var canBeEquippedBehavior))
                    {
                        Debug.Log($"Cannot unequip '{item}' to drop.");
                        return false;
                    }

                    return true;
                });
        }
    }
}