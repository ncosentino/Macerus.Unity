using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
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

        public IPrefabCreator PrefabCreator { get; set; }

        public IObjectDestroyer ObjectDestroyer { get; set; }

        public string DragItemPrefabResource { get; set; }

        public void Start()
        {
            Contract.RequiresNotNull(
                InventoryGameObject,
                $"{nameof(InventoryGameObject)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                PrefabCreator,
                $"{nameof(PrefabCreator)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                ObjectDestroyer,
                $"{nameof(ObjectDestroyer)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNullOrEmpty(
                DragItemPrefabResource,
                $"{nameof(DragItemPrefabResource)} was not set on '{gameObject}.{this}'.");
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (_inventoryTransform == null)
            {
                _inventoryTransform = GameObject.Find("Inventory").transform;
            }

            if (_dragObject == null)
            {
                _dragObject = PrefabCreator.Create<GameObject>(DragItemPrefabResource);
                _dragObject.name = $"Drag Item";

                var sourceIcon = gameObject.GetRequiredComponentInChild<Image>("Icon");
                var sprite = sourceIcon.sprite;
                var targetIcon = _dragObject.GetRequiredComponentInChild<Image>("Icon");
                targetIcon.sprite = sprite;
                targetIcon.color = new Color(
                    sourceIcon.color.r,
                    sourceIcon.color.g,
                    sourceIcon.color.b,
                    sourceIcon.color.a * 2f / 3f);
                _dragObject.transform.SetParent(_inventoryTransform);
            }

            // TODO: inject an interface backed by unity Input.mousePosition for this
            _dragObject.transform.position = UnityEngine.Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ObjectDestroyer.Destroy(_dragObject);
            _dragObject = null;
        }
    }
}
