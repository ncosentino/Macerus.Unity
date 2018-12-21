using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IItemListFactory
    {
        IItemListPrefab Create(
            string itemListPrefabResource,
            string itemListItemPrefabResource,
            IItemContainerBehavior itemContainerBehavior);
    }
}