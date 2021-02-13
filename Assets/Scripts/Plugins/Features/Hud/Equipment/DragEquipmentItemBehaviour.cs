using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;
using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;

using ProjectXyz.Framework.Contracts;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment
{
    public class DragEquipmentItemBehaviour :
        MonoBehaviour,
        IDragEquipmentItemBehaviour,
        IDragHandler,
        IEndDragHandler
    {
        private IDragItemPrefab _dragObject;

        public GameObject InventoryGameObject { get; set; }

        public IDragItemFactory DragItemFactory { get; set; }

        public IObjectDestroyer ObjectDestroyer { get; set; }

        public IEquipSlotPrefab EquipSlot { get; set; }

        public void Start()
        {
            Contract.RequiresNotNull(
                InventoryGameObject,
                $"{nameof(InventoryGameObject)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                EquipSlot,
                $"{nameof(EquipSlot)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                DragItemFactory,
                $"{nameof(DragItemFactory)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                ObjectDestroyer,
                $"{nameof(ObjectDestroyer)} was not set on '{gameObject}.{this}'.");
        }

        public void OnDestroy()
        {
            ObjectDestroyer.Destroy(_dragObject?.GameObject);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (_dragObject == null)
            {
                _dragObject = DragItemFactory.Create(EquipSlot.ActiveIcon);
                _dragObject.SetParent(InventoryGameObject.transform);
            }

            // TODO: inject an interface backed by unity Input.mousePosition for this
            _dragObject
                .GameObject
                .transform
                .position = UnityEngine.Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("ON END DRAG");

            var droppedOnCanvas = eventData
                .pointerCurrentRaycast
                .gameObject == null;
            if (droppedOnCanvas)
            {
                TryUnequipAndDropItem(eventData);
            }

            ObjectDestroyer.Destroy(_dragObject.GameObject);
            _dragObject = null;
        }

        private bool TryUnequipAndDropItem(PointerEventData eventData)
        {
            var dropEquipmentSlotBehaviour = eventData
                .pointerDrag
                .GetComponent<IDropEquipmentSlotBehaviour>();
            Contract.RequiresNotNull(
                dropEquipmentSlotBehaviour,
                $"'{eventData.pointerDrag}' does not have " +
                $"'{nameof(IReadOnlyHasGameObject)}' as a component");

            var item = eventData
                .pointerDrag
                .GetComponent<IReadOnlyHasGameObject>()
                ?.GameObject;
            Contract.RequiresNotNull(
                item,
                $"'{dropEquipmentSlotBehaviour}' does not have " +
                $"'{nameof(IReadOnlyHasGameObject)}' as a sibling component " +
                $"with a game object set.");

            // FIXME: check if we can drop the item here

            if (!dropEquipmentSlotBehaviour
                .CanEquipBehavior
                .TryUnequip(
                    dropEquipmentSlotBehaviour.TargetEquipSlotId,
                    out var canBeEquippedBehavior))
            {
                Debug.Log($"Cannot unequip '{item}' to drop.");
                return false;
            }

            // FIXME: actually put the item on the ground?

            Debug.Log($"Unequipped '{item}' and DESTROYED it.");
            return true;
        }
    }
}
