using System.Linq;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using ProjectXyz.Api.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
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