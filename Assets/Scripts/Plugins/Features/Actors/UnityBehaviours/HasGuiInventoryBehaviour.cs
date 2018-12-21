using System;
using System.Linq;
using Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory;
using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Framework.Contracts;
using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;
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

        public IViewWelderFactory ViewWelderFactory { get; set; }

        private IItemListPrefab _itemList;

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
            Contract.RequiresNotNull(
                ViewWelderFactory,
                $"{nameof(ViewWelderFactory)} was not set on '{gameObject}.{this}'.");

            // FIXME: find a better way to associate an item collection to the thing in the UI
            var inventoryBagUi = GameObjectManager
                .FindAll(x => x.name == "Bag")
                .First();

            _itemList = ItemListFactory.Create(
                "Gui/Prefabs/Inventory/ItemList",
                "Gui/Prefabs/Inventory/InventoryListItem",
                ItemContainerBehavior);

            ViewWelderFactory
                .Create<IInsetWelder>(inventoryBagUi, _itemList.GameObject)
                .Weld(new InsetWeldOptions(5));
        }

        private void OnDestroy()
        {
            ObjectDestroyer.Destroy(_itemList.GameObject);
        }
    }
}