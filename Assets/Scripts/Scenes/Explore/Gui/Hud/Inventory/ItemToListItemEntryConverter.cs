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
        private readonly IReadOnlyCollection<IInventoryListItemMutator> _inventoryListItemMutators;

        public ItemToListItemEntryConverter(
            IPrefabCreator prefabCreator,
            IHasGameObjectBehaviourStitcher hasGameObjectBehaviourStitcher,
            IEnumerable<IInventoryListItemMutator> inventoryListItemMutators)
        {
            _prefabCreator = prefabCreator;
            _hasGameObjectBehaviourStitcher = hasGameObjectBehaviourStitcher;
            _inventoryListItemMutators = inventoryListItemMutators.ToArray();
        }

        public GameObject Convert(
            IGameObject item,
            string itemListEntryPrefabResource)
        {
            var itemEntry = _prefabCreator.Create<GameObject>(itemListEntryPrefabResource);
            _hasGameObjectBehaviourStitcher.Attach(
                item,
                itemEntry);

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