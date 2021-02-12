using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Framework.Contracts;

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
                _dragObject
                    .GameObject
                    .transform
                    .SetParent(InventoryGameObject.transform);
            }

            // FIXME: inject an interface backed by unity Input.mousePosition for this
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
