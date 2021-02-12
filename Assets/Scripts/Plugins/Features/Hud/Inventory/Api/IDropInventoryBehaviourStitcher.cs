using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IDropInventoryBehaviourStitcher
    {
        IReadOnlyDropInventoryBehaviour Attach(
            IItemListPrefab itemListPrefab,
            IItemContainerBehavior itemContainerBehavior);
    }
}