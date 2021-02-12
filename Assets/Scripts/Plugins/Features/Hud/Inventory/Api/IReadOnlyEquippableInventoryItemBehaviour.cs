using System;

using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;

using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IReadOnlyEquippableInventoryItemBehaviour : IReadOnlyEquippableItemBehaviour
    {
        Func<IItemContainerBehavior> SourceItemContainerCallback { get; }
    }
}