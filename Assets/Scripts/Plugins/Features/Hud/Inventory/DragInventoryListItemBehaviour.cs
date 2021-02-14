using Assets.Scripts.Plugins.Features.Hud.Api;
using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;
using Assets.Scripts.Unity.Resources.Prefabs;

using Macerus.Api.Behaviors;
using Macerus.Game.GameObjects;

using ProjectXyz.Api.GameObjects;
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

        public IDropItemHandler DropItemHandler { get; set; }

        public IMouseInput MouseInput { get; set; }

        public IGameObjectManager GameObjectManager { get; set; }

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
            var droppedOnCanvas = eventData
                .pointerCurrentRaycast
                .gameObject == null;
            if (droppedOnCanvas)
            {
                TryDropItem(eventData);
            }

            ObjectDestroyer.Destroy(_dragObject.GameObject);
            _dragObject = null;
        }

        private bool TryDropItem(PointerEventData eventData)
        {
            var inventoryItemBehaviour = eventData
                .pointerDrag
                .GetComponent<IInventoryItemBehaviour>();
            Contract.RequiresNotNull(
                inventoryItemBehaviour,
                $"'{eventData.pointerDrag}' does not have " +
                $"'{nameof(inventoryItemBehaviour)}' as a component");

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
