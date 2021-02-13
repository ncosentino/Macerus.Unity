using System.Linq;

using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources.Prefabs;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class InventoryListItemEquippableMutator : IInventoryListItemMutator
    {
        public void Mutate(
            IInventoryListItemPrefab inventoryListItem,
            IGameObject item)
        {
            var inventoryItemBehaviour = inventoryListItem
                .AddComponent<InventoryItemBehaviour>();
            inventoryItemBehaviour.InventoryItem = item;
            inventoryItemBehaviour.SourceItemContainerCallback = () =>
            {
                var sourceItemContainer = inventoryListItem
                    .GameObject
                    .GetRequiredComponent<ISourceItemContainerBehaviour>()
                    .SourceItemContainer;
                return sourceItemContainer;
            };

            var canBeEquippedBehavior = item
                .Get<ICanBeEquippedBehavior>()
                .SingleOrDefault();
            if (canBeEquippedBehavior == null)
            {
                return;
            }

            var equippableInventoryItemBehaviour = inventoryListItem
                .AddComponent<EquippableInventoryItemBehaviour>();
            equippableInventoryItemBehaviour.InventoryItemBehaviour = inventoryItemBehaviour;
            equippableInventoryItemBehaviour.CanBeEquippedBehavior = canBeEquippedBehavior;
        }
    }
}