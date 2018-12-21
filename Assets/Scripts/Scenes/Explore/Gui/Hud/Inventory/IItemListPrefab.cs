using Assets.Scripts.Unity.Resources;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IItemListPrefab : IPrefab
    {
        GameObject Content { get; }
    }
}