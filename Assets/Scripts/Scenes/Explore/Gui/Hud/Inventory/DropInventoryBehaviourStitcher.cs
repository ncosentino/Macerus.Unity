using Assets.Scripts.Unity.Resources;

using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public sealed class DropInventoryBehaviourStitcher : IDropInventoryBehaviourStitcher
    {
        public IReadOnlyDropInventoryBehaviour Attach(
            IItemListPrefab itemListPrefab,
            IItemContainerBehavior itemContainerBehavior)
        {
            var dropInventoryBehaviour = itemListPrefab.AddComponent<DropInventoryBehaviour>();

            dropInventoryBehaviour.ItemContainerBehavior = itemContainerBehavior;

            return dropInventoryBehaviour;
        }
    }
}