using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public interface IItemListBehaviourStitcher
    {
        IReadonlyItemListBehaviour Attach(
            IItemListPrefab listControl,
            string itemListEntryPrefabResource,
            IItemContainerBehavior itemContainerBehavior);
    }
}