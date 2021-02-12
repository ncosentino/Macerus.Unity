using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Plugins.Features.Hud.Equipment;

using ProjectXyz.Framework.Contracts;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public class DropInventoryBehaviour :
        MonoBehaviour,
        IDropHandler, IDropInventoryBehaviour
    {
        public IItemContainerBehavior ItemContainerBehavior { get; set; }

        public void Start()
        {
            Contract.RequiresNotNull(
                ItemContainerBehavior,
                $"{nameof(ItemContainerBehavior)} was not set on '{gameObject}.{this}'.");
        }

        public void OnDrop(PointerEventData eventData)
        {
            var dropEquipmentSlotBehaviour = eventData
                .pointerDrag
                .GetComponent<IDropEquipmentSlotBehaviour>();
            if (dropEquipmentSlotBehaviour == null)
            {
                return;
            }

            UnequipAndAddItem(eventData, dropEquipmentSlotBehaviour);
        }

        private void UnequipAndAddItem(
            PointerEventData eventData,
            IDropEquipmentSlotBehaviour dropEquipmentSlotBehaviour)
        {
            var item = eventData
                .pointerDrag
                .GetComponent<IReadOnlyHasGameObject>()
                ?.GameObject;
            Contract.RequiresNotNull(
                item,
                $"'{dropEquipmentSlotBehaviour}' does not have " +
                $"'{nameof(IReadOnlyHasGameObject)}' as a sibling component " +
                $"with a game object set.");

            if (!ItemContainerBehavior.CanAddItem(item))
            {
                Debug.Log($"Cannot unequip '{item}' to add to '{ItemContainerBehavior}'.");
                return;
            }

            if (!dropEquipmentSlotBehaviour
                .CanEquipBehavior
                .TryUnequip(
                    dropEquipmentSlotBehaviour.TargetEquipSlotId,
                    out var canBeEquippedBehavior))
            {
                Debug.Log($"Cannot unequip '{item}' to add to '{ItemContainerBehavior}'.");
            }

            if (!ItemContainerBehavior.TryAddItem(item))
            {
                Debug.Log($"Cannot unequip '{item}' to add to '{ItemContainerBehavior}'.");

                // FIXME: need to re-equip??? What if the thing you had equipped 
                // gave you container capacity and now you can't add it to the
                // container??
                return;
            }

            Debug.Log($"Unequipped '{item}'.");
        }
    }
}
