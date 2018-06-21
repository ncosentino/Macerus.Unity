using System.Linq;
using Assets.Scripts.Plugins.Features.Actors;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using Assets.Scripts.Wip;
using UnityEngine;

namespace Assets.Scripts.Gui.Hud.Inventory
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

        public GameObject Create(
            string itemListPrefabResource,
            string itemListItemPrefabResource,
            IItemContainer itemContainer)
        {
            var itemListGameObject = _prefabCreator.Create<GameObject>(itemListPrefabResource);

            // TODO: intelligently look up content control via... behavior or something?
            var itemListContent = itemListGameObject
                .GetChildGameObjects()
                .Single(x => x.name == "ItemListContent");

            _itemListBehaviourStitcher.Attach(
                itemListGameObject,
                itemListContent,
                itemListItemPrefabResource,
                itemContainer);
            return itemListGameObject;
        }
    }
}