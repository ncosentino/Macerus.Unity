using UnityEngine;

namespace Assets.Scripts.Gui.Hud.Inventory
{
    public interface IItemListFactory
    {
        GameObject Create(
            string itemListPrefabResource,
            string itemListItemPrefabResource);
    }
}