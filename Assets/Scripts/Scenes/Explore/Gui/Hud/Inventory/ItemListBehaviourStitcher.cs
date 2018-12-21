using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public sealed class ItemListBehaviourStitcher : IItemListBehaviourStitcher
    {
        private readonly IItemToListItemEntryConverter _itemToListItemEntryConverter;
        private readonly IObjectDestroyer _objectDestroyer;

        public ItemListBehaviourStitcher(
            IItemToListItemEntryConverter itemToListItemEntryConverter,
            IObjectDestroyer objectDestroyer)
        {
            _itemToListItemEntryConverter = itemToListItemEntryConverter;
            _objectDestroyer = objectDestroyer;
        }

        public IReadonlyItemListBehaviour Attach(
            IItemListPrefab listControl,
            string itemListEntryPrefabResource,
            IItemContainerBehavior itemContainerBehavior)
        {
            var itemListBehaviour = listControl.GameObject.AddComponent<ItemListBehaviour>();
            itemListBehaviour.ItemListEntryPrefabResource = itemListEntryPrefabResource;
            itemListBehaviour.ItemToListItemEntryConverter = _itemToListItemEntryConverter;
            itemListBehaviour.ListControlContent = listControl.Content;
            itemListBehaviour.ItemContainerBehavior = itemContainerBehavior;
            itemListBehaviour.ObjectDestroyer = _objectDestroyer;

            return itemListBehaviour;
        }
    }
}
