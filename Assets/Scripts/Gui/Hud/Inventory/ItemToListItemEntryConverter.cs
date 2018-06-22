using System.Linq;
using Assets.Scripts.Scenes.Explore.GameObjects;
using Assets.Scripts.Unity.Resources;
using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Gui.Hud.Inventory
{
    public sealed class ItemToListItemEntryConverter : IItemToListItemEntryConverter
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly IHasGameObjectBehaviourStitcher _hasGameObjectBehaviourStitcher;
        

        public ItemToListItemEntryConverter(
            IPrefabCreator prefabCreator,
            IHasGameObjectBehaviourStitcher hasGameObjectBehaviourStitcher)
        {
            _prefabCreator = prefabCreator;
            _hasGameObjectBehaviourStitcher = hasGameObjectBehaviourStitcher;
        }

        public GameObject Convert(
            IGameObject item,
            string itemListEntryPrefabResource)
        {
            var itemEntry = _prefabCreator.Create<GameObject>(itemListEntryPrefabResource);
            _hasGameObjectBehaviourStitcher.Attach(
                item,
                itemEntry);

            // TODO: set the name

            // TODO: set the icon

            // TODO: display colours differently

            return itemEntry;
        }
    }
}