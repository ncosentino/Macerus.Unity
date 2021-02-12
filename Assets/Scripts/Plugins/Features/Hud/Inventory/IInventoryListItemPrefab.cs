using Assets.Scripts.Unity.Resources;
using UnityEngine.UI;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public interface IInventoryListItemPrefab : IPrefab
    {
        Image Icon { get; }

        Text Name { get; }
    }
}