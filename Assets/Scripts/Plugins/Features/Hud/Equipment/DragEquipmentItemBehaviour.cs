using System;

using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;
using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;
using Assets.Scripts.Unity.Resources.Prefabs;

using NexusLabs.Contracts;

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

        public IMouseInput MouseInput { get; set; }

        public event EventHandler<DroppedEventArgs> Dropped;

        public void Start()
        {
            this.RequiresNotNull(MouseInput, nameof(MouseInput));
            this.RequiresNotNull(ObjectDestroyer, nameof(ObjectDestroyer));
            this.RequiresNotNull(DragItemFactory, nameof(DragItemFactory));
            this.RequiresNotNull(EquipSlot, nameof(EquipSlot));
            this.RequiresNotNull(InventoryGameObject, nameof(InventoryGameObject));
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

            _dragObject
                .GameObject
                .transform
                .position = MouseInput.Position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            try
            {
                Dropped?.Invoke(
                    this,
                    new DroppedEventArgs(
                        eventData.pointerDrag,
                        eventData.pointerCurrentRaycast.gameObject));
            }
            finally
            {
                ObjectDestroyer.Destroy(_dragObject.GameObject);
                _dragObject = null;
            }
        }
    }
}
