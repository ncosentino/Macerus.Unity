﻿using Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using ProjectXyz.Framework.Contracts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
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
            ObjectDestroyer.Destroy(_dragObject.GameObject);
            _dragObject = null;
        }
    }
}
