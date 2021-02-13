using System;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IInventoryItemBehaviour : IReadOnlyInventoryItemBehaviour
    {
        new IGameObject InventoryItem { get; set; }

        new Func<IItemContainerBehavior> SourceItemContainerCallback { get; set; }
    }
}