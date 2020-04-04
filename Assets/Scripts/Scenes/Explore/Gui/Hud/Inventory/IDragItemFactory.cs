using UnityEngine.UI;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IDragItemFactory
    {
        IDragItemPrefab Create(Image sourceIcon);
    }
}