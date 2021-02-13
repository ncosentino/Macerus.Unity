using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.Resources.Prefabs;

using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
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