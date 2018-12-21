using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IDragItemFactory
    {
        GameObject Create(
            Image sourceIcon,
            IItemContainerBehavior sourceContainerBehavior);
    }
}