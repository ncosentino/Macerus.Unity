using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

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
            GameObject listControl,
            GameObject listControlContent,
            string itemListEntryPrefabResource,
            IItemContainerBehavior itemContainerBehavior)
        {
            var itemListBehaviour = listControl.AddComponent<ItemListBehaviour>();
            itemListBehaviour.ItemListEntryPrefabResource = itemListEntryPrefabResource;
            itemListBehaviour.ItemToListItemEntryConverter = _itemToListItemEntryConverter;
            itemListBehaviour.ListControlContent = listControlContent;
            itemListBehaviour.ItemContainerBehavior = itemContainerBehavior;
            itemListBehaviour.ObjectDestroyer = _objectDestroyer;

            return itemListBehaviour;
        }
    }
}
