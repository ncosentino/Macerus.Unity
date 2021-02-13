using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IItemListPrefab : IPrefab
    {
        GameObject Content { get; }
    }
}