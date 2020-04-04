using System;
using Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IReadOnlyEquippableInventoryItemBehaviour : IReadOnlyEquippableItemBehaviour
    {
        Func<IItemContainerBehavior> SourceItemContainerCallback { get; }
    }
}