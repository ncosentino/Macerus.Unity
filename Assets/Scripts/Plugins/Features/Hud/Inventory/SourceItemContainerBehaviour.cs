using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;

using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class SourceItemContainerBehaviour :
        MonoBehaviour,
        ISourceItemContainerBehaviour
    {
        public IItemContainerBehavior SourceItemContainer { get; set; }
    }
}