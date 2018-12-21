using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public sealed class SourceItemContainerBehaviour :
        MonoBehaviour,
        ISourceItemContainerBehaviour
    {
        public IItemContainerBehavior SourceItemContainer { get; set; }
    }
}