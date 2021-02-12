using UnityEngine.UI;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public interface IDragItemFactory
    {
        IDragItemPrefab Create(Image sourceIcon);
    }
}