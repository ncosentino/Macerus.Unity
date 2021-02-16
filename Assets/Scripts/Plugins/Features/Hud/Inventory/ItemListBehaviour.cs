using System.Collections.Generic;

using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources.Prefabs;

using ProjectXyz.Api.GameObjects;
using NexusLabs.Contracts;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class ItemListBehaviour :
        MonoBehaviour,
        IItemListBehaviour
    {
        private readonly Dictionary<IGameObject, IInventoryListItemPrefab> _listItems;

        public ItemListBehaviour()
        {
            _listItems = new Dictionary<IGameObject, IInventoryListItemPrefab>();
        }

        public IItemToListItemEntryConverter ItemToListItemEntryConverter { get; set; }

        public string ItemListEntryPrefabResource { get; set; }

        public GameObject ListControlContent { get; set; }

        public IItemContainerBehavior ItemContainerBehavior { get; set; }

        public IObjectDestroyer ObjectDestroyer { get; set; }

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
            Contract.RequiresNotNull(
                ObjectDestroyer,
                $"{nameof(ObjectDestroyer)} was not set on '{gameObject}.{this}'.");

            ItemContainerBehavior.ItemsChanged += ItemContainerBehavior_ItemsChanged;
            AddItems(ItemContainerBehavior.Items);
        }

        private void OnDestroy()
        {
            if (ItemContainerBehavior != null)
            {
                ItemContainerBehavior.ItemsChanged -= ItemContainerBehavior_ItemsChanged;
            }
        }

        private void AddItems(IEnumerable<IGameObject> items)
        {
            foreach (var item in items)
            {
                var listItem = ItemToListItemEntryConverter.Convert(
                    item,
                    ItemListEntryPrefabResource);
                listItem.SetParent(ListControlContent.transform);

                // TODO: technically... this should be stitched :shrug:
                var sourceItemContainerBehaviour = listItem.AddComponent<SourceItemContainerBehaviour>();
                sourceItemContainerBehaviour.SourceItemContainer = ItemContainerBehavior;

                _listItems.Add(item, listItem);
            }
        }

        private void RemoveItems(IEnumerable<IGameObject> items)
        {
            foreach (var item in items)
            {
                var listItem = _listItems[item];
                _listItems.Remove(item);
                ObjectDestroyer.Destroy(listItem.GameObject);
            }
        }

        private void ItemContainerBehavior_ItemsChanged(object sender, ItemsChangedEventArgs e)
        {
            AddItems(e.AddedItems);
            RemoveItems(e.RemovedItems);
        }
    }
}