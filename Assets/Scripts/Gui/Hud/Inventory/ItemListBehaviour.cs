using ProjectXyz.Framework.Contracts;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Gui.Hud.Inventory
{
    public sealed class ItemListBehaviour :
        MonoBehaviour,
        IItemListBehaviour
    {
        public IItemToListItemEntryConverter ItemToListItemEntryConverter { get; set; }

        public string ItemListEntryPrefabResource { get; set; }

        public GameObject ListControlContent { get; set; }

        public IItemContainerBehavior ItemContainerBehavior { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                ItemToListItemEntryConverter,
                $"{nameof(ItemToListItemEntryConverter)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNullOrEmpty(
                ItemListEntryPrefabResource,
                $"{nameof(ItemListEntryPrefabResource)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                ListControlContent,
                $"{nameof(ListControlContent)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                ItemContainerBehavior,
                $"{nameof(ItemContainerBehavior)} was not set on '{gameObject}.{this}'.");

            // TODO: make this observable so we can bind to an event
            // TODO: REMEMBER TO UNHOOK IT IN THE OnDestroy() METHOD!!!!!!!!!!!!!!!
            foreach (var item in ItemContainerBehavior.Items)
            {
                var itemEntry = ItemToListItemEntryConverter.Convert(
                    item,
                    ItemListEntryPrefabResource);
                itemEntry.transform.SetParent(ListControlContent.transform, false);
            }
        }

        private void Update()
        {
        }
    }
}