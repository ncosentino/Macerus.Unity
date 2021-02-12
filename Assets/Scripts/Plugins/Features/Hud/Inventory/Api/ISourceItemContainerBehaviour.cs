using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface ISourceItemContainerBehaviour : IReadOnlySourceItemContainerBehaviour
    {
        new IItemContainerBehavior SourceItemContainer { get; set; }
    }
}