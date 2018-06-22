using System.Linq;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using ProjectXyz.Api.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Hud.Inventory
{
    public sealed class ItemToListItemEntryConverter : IItemToListItemEntryConverter
    {
        private readonly IPrefabCreator _prefabCreator;

        public ItemToListItemEntryConverter(IPrefabCreator prefabCreator)
        {
            _prefabCreator = prefabCreator;
        }

        public GameObject Convert(
            IGameObject item,
            string itemListEntryPrefabResource)
        {
            var itemEntry = _prefabCreator.Create<GameObject>(itemListEntryPrefabResource);
            

            return itemEntry;
        }
    }
}