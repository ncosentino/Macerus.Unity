using Assets.Scripts.Plugins.Features.Actors;
using Assets.Scripts.Unity.Resources;
using Assets.Scripts.Wip;
using UnityEngine;

namespace Assets.Scripts.Gui.Hud.Inventory
{
    public sealed class ItemListBehaviourStitcher : IItemListBehaviourStitcher
    {
        private readonly IPrefabCreator _prefabCreator;

        public ItemListBehaviourStitcher(IPrefabCreator prefabCreator)
        {
            _prefabCreator = prefabCreator;
        }

        public IReadonlyItemListBehaviour Attach(
            GameObject listControl,
            GameObject listControlContent,
            string itemListEntryPrefabResource,
            IItemContainer itemContainer)
        {
            var itemListBehaviour = listControl.AddComponent<ItemListBehaviour>();
            itemListBehaviour.ItemListEntryPrefabResource = itemListEntryPrefabResource;
            itemListBehaviour.PrefabCreator = _prefabCreator;
            itemListBehaviour.ListControlContent = listControlContent;
            itemListBehaviour.ItemContainer = itemContainer;

            return itemListBehaviour;
        }
    }
}
