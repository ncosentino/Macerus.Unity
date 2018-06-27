using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IItemListBehaviourStitcher
    {
        IReadonlyItemListBehaviour Attach(
            GameObject listControl,
            GameObject listControlContent,
            string itemListEntryPrefabResource,
            IItemContainerBehavior itemContainerBehavior);
    }
}