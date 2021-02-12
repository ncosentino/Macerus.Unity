using System.Linq;

using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class InventoryListItemNameMutator : IInventoryListItemMutator
    {
        public void Mutate(
            IInventoryListItemPrefab inventoryListItemPrefab,
            IGameObject item)
        {
            var hasInventoryDisplayName = item
                .Get<IHasInventoryDisplayName>()
                .LastOrDefault();
            if (hasInventoryDisplayName == null)
            {
                return;
            }

            inventoryListItemPrefab
                .Name
                .text = hasInventoryDisplayName.DisplayName;
        }
    }
}