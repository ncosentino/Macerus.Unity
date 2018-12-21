using Assets.Scripts.Unity.Resources;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public sealed class ItemListFactory : IItemListFactory
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly IItemListBehaviourStitcher _itemListBehaviourStitcher;

        public ItemListFactory(
            IPrefabCreator prefabCreator,
            IItemListBehaviourStitcher itemListBehaviourStitcher)
        {
            _prefabCreator = prefabCreator;
            _itemListBehaviourStitcher = itemListBehaviourStitcher;
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
            return itemListGameObject;
        }
    }
}