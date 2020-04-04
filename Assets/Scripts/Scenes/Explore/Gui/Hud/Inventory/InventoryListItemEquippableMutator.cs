using System.Linq;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public sealed class InventoryListItemEquippableMutator : IInventoryListItemMutator
    {
        public void Mutate(
            IInventoryListItemPrefab inventoryListItem,
            IGameObject item)
        {
            var canBeEquippedBehavior = item
                .Get<ICanBeEquippedBehavior>()
                .SingleOrDefault();
            if (canBeEquippedBehavior == null)
            {
                return;
            }

            var equippableInventoryItemBehaviour =
                inventoryListItem.AddComponent<EquippableInventoryItemBehaviour>();
            equippableInventoryItemBehaviour.SourceItemContainerCallback = () =>
            {
                var sourceItemContainer = inventoryListItem
                    .GameObject
                    .GetRequiredComponent<ISourceItemContainerBehaviour>()
                    .SourceItemContainer;
                return sourceItemContainer;
            };
            equippableInventoryItemBehaviour.CanBeEquippedBehavior = canBeEquippedBehavior;
        }
    }
}