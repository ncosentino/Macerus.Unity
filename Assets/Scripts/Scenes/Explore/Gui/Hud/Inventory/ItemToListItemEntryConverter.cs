using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Scenes.Explore.GameObjects;
using Assets.Scripts.Unity.Resources;
using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public sealed class ItemToListItemEntryConverter : IItemToListItemEntryConverter
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly IHasGameObjectBehaviourStitcher _hasGameObjectBehaviourStitcher;
        private readonly IDragInventoryListItemBehaviourStitcher _dragInventoryListItemBehaviourStitcher;
        private readonly IReadOnlyCollection<IInventoryListItemMutator> _inventoryListItemMutators;

        public ItemToListItemEntryConverter(
            IPrefabCreator prefabCreator,
            IHasGameObjectBehaviourStitcher hasGameObjectBehaviourStitcher,
            IDragInventoryListItemBehaviourStitcher dragInventoryListItemBehaviourStitcher,
            IEnumerable<IInventoryListItemMutator> inventoryListItemMutators)
        {
            _prefabCreator = prefabCreator;
            _hasGameObjectBehaviourStitcher = hasGameObjectBehaviourStitcher;
            _dragInventoryListItemBehaviourStitcher = dragInventoryListItemBehaviourStitcher;
            _inventoryListItemMutators = inventoryListItemMutators.ToArray();
        }

        public GameObject Convert(
            IGameObject item,
            string itemListEntryPrefabResource)
        {
            var itemEntry = _prefabCreator.Create<GameObject>(itemListEntryPrefabResource);
            itemEntry.name = $"Inventory Item: {item}";

            _hasGameObjectBehaviourStitcher.Attach(
                item,
                itemEntry);
            _dragInventoryListItemBehaviourStitcher.Attach(itemEntry);

            foreach (var mutator in _inventoryListItemMutators)
            {
                mutator.Mutate(
                    itemEntry,
                    item);
            }

            //// TODO: set the icon via a mutator;

            return itemEntry;
        }
    }
}