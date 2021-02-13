using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine.UI;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IDragItemPrefab : IPrefab
    {
        Image Icon { get; }
    }
}