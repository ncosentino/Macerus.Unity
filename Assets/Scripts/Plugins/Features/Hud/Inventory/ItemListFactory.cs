using Assets.Scripts.Unity.Resources;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class ItemListFactory : IItemListFactory
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly IItemListBehaviourStitcher _itemListBehaviourStitcher;
        private readonly IDropInventoryBehaviourStitcher _dropInventoryBehaviourStitcher;

        public ItemListFactory(
            IPrefabCreator prefabCreator,
            IItemListBehaviourStitcher itemListBehaviourStitcher,
            IDropInventoryBehaviourStitcher dropInventoryBehaviourStitcher)
        {
            _prefabCreator = prefabCreator;
            _itemListBehaviourStitcher = itemListBehaviourStitcher;
            _dropInventoryBehaviourStitcher = dropInventoryBehaviourStitcher;
        }

        public IItemListPrefab Create(
            string itemListPrefabResource,
            string itemListItemPrefabResource,
            IItemContainerBehavior itemContainerBehavior)
        {
            var itemListGameObject = _prefabCreator.CreatePrefab<IItemListPrefab>(itemListPrefabResource);

            _itemListBehaviourStitcher.Attach(
                itemListGameObject,
                itemListItemPrefabResource,
                itemContainerBehavior);
            _dropInventoryBehaviourStitcher.Attach(
                itemListGameObject,
                itemContainerBehavior);

            return itemListGameObject;
        }
    }
}