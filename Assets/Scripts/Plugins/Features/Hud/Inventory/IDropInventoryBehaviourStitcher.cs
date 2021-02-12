using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public interface IDropInventoryBehaviourStitcher
    {
        IReadOnlyDropInventoryBehaviour Attach(
            IItemListPrefab itemListPrefab,
            IItemContainerBehavior itemContainerBehavior);
    }
}