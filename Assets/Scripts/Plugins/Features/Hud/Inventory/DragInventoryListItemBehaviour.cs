using System;

using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;
using Assets.Scripts.Unity.Resources.Prefabs;

using NexusLabs.Contracts;

using UnityEngine;
using UnityEngine.EventSystems;

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

        public IMouseInput MouseInput { get; set; }

        public event EventHandler<DroppedEventArgs> Dropped;

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
                MouseInput,
                $"{nameof(MouseInput)} was not set on '{gameObject}.{this}'.");
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
