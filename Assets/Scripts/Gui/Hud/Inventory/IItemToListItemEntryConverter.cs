using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Gui.Hud.Inventory
{
    public interface IItemToListItemEntryConverter
    {
        GameObject Convert(
            IGameObject item,
            string itemListEntryPrefabResource);
    }
}