using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public interface IReadOnlySourceItemContainerBehaviour
    {
        IItemContainerBehavior SourceItemContainer { get; }
    }
}