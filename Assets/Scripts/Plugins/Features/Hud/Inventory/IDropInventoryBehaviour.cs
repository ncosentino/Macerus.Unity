using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public interface IDropInventoryBehaviour : IReadOnlyDropInventoryBehaviour
    {
        new IItemContainerBehavior ItemContainerBehavior { get; set; }
    }
}