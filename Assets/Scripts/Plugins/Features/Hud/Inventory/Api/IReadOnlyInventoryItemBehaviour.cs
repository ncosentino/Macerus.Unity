using System;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IReadOnlyInventoryItemBehaviour
    {
        IGameObject InventoryItem { get; }

        Func<IItemContainerBehavior> SourceItemContainerCallback { get; }

        bool TryRemoveFromSourceContainer();
    }
}