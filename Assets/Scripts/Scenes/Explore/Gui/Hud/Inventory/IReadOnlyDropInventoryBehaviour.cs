using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IReadOnlyDropInventoryBehaviour
    {
        IItemContainerBehavior ItemContainerBehavior { get; }
    }
}