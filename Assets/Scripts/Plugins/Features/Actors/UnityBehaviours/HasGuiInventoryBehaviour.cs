using System.Linq;
using Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory;
using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Framework.Contracts;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public sealed class HasGuiInventoryBehaviour :
        MonoBehaviour,
        IHasGuiInventoryBehaviour
    {
        public IItemContainerBehavior ItemContainerBehavior { get; set; }

        public IItemListFactory ItemListFactory { get; set; }

        public IObjectDestroyer ObjectDestroyer { get; set; }

        public IGameObjectManager GameObjectManager { get; set; }

        private GameObject _itemList;

        private void Start()
        {
            Contract.RequiresNotNull(
                ItemContainerBehavior,
                $"{nameof(ItemContainerBehavior)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                ItemListFactory,
                $"{nameof(ItemListFactory)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                ObjectDestroyer,
                $"{nameof(ObjectDestroyer)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                GameObjectManager,
                $"{nameof(GameObjectManager)} was not set on '{gameObject}.{this}'.");

            // FIXME: find a better way to associate an item collection to the thing in the UI
            var inventoryBagUi = GameObjectManager
                .FindAll(x => x.name == "Bag")
                .First();

            _itemList = ItemListFactory.Create(
                "Gui/Prefabs/Inventory/ItemList",
                "Gui/Prefabs/Inventory/InventoryListItem",
                ItemContainerBehavior);
            _itemList.transform.SetParent(inventoryBagUi.transform, false);

            // set margin
            var transform = _itemList.GetComponent<RectTransform>();
            transform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 5, transform.rect.width - 5);
            transform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 5, transform.rect.width - 5);
            transform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 5, transform.rect.height - 5);
            transform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 5, transform.rect.height - 5);
        }

        private void OnDestroy()
        {
            ObjectDestroyer.Destroy(_itemList);
        }
    }
}