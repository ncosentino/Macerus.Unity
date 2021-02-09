using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IDropInventoryBehaviour : IReadOnlyDropInventoryBehaviour
    {
        new IItemContainerBehavior ItemContainerBehavior { get; set; }
    }
}