using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Gui.Hud.Inventory
{
    public sealed class ItemListBehaviourStitcher : IItemListBehaviourStitcher
    {
        private readonly IItemToListItemEntryConverter _itemToListItemEntryConverter;

        public ItemListBehaviourStitcher(IItemToListItemEntryConverter itemToListItemEntryConverter)
        {
            _itemToListItemEntryConverter = itemToListItemEntryConverter;
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

            return itemListBehaviour;
        }
    }
}
