using Assets.Scripts.Unity.Resources;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IDragItemPrefab : IPrefab
    {
        Image Icon { get; }
    }
}