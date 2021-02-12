using Assets.Scripts.Unity.Resources;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public interface IItemListPrefab : IPrefab
    {
        GameObject Content { get; }
    }
}