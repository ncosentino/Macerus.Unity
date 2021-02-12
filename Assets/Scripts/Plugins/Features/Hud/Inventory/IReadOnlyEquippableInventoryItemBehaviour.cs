using System;

using Assets.Scripts.Plugins.Features.Hud.Equipment;

using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public interface IReadOnlyEquippableInventoryItemBehaviour : IReadOnlyEquippableItemBehaviour
    {
        Func<IItemContainerBehavior> SourceItemContainerCallback { get; }
    }
}