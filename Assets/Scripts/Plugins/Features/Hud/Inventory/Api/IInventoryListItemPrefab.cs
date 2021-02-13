using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine.UI;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IInventoryListItemPrefab : IPrefab
    {
        Image Icon { get; }

        Text Name { get; }
    }
}