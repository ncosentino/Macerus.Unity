using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Framework.Contracts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public class DragInventoryListItemBehaviour :
        MonoBehaviour,
        IDragInventoryListItemBehaviour,
        IDragHandler,
        IEndDragHandler
    {
        private Transform _inventoryTransform;
        private GameObject _dragObject;

        public GameObject InventoryGameObject { get; set; }

        public IDragItemFactory DragItemFactory { get; set; }

        public IObjectDestroyer ObjectDestroyer { get; set; }

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
        }

        public void OnDestroy()
        {
            ObjectDestroyer.Destroy(_dragObject);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (_inventoryTransform == null)
            {
                _inventoryTransform = GameObject.Find("Inventory").transform;
            }

            if (_dragObject == null)
            {
                var sourceIcon = gameObject.GetRequiredComponentInChild<Image>("Icon");
                var itemListBehaviour = InventoryGameObject
                    .GetComponentInChildren<IReadonlyItemListBehaviour>()
                    .ItemContainerBehavior;
                _dragObject = DragItemFactory.Create(
                    sourceIcon,
                    itemListBehaviour);
                _dragObject.transform.SetParent(_inventoryTransform);
            }

            // TODO: inject an interface backed by unity Input.mousePosition for this
            _dragObject.transform.position = UnityEngine.Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("ON END DRAG");
            ObjectDestroyer.Destroy(_dragObject);
            _dragObject = null;
        }
    }
}
