using System;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public interface IEquippableInventoryItemBehaviour : IReadOnlyEquippableInventoryItemBehaviour
    {
        new Func<IItemContainerBehavior> SourceItemContainerCallback { get; set; }
    }
}