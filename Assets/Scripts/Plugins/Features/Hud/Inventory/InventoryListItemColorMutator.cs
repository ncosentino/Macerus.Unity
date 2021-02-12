using System.Linq;

using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class InventoryListItemColorMutator : IInventoryListItemMutator
    {
        public void Mutate(
            IInventoryListItemPrefab inventoryListItemPrefab,
            IGameObject item)
        {
            var hasInventoryDisplayColor = item
                .Get<IHasInventoryDisplayColor>()
                .SingleOrDefault();
            if (hasInventoryDisplayColor == null)
            {
                return;
            }

            var color = new Color(
                hasInventoryDisplayColor.R / 255f,
                hasInventoryDisplayColor.G / 255f,
                hasInventoryDisplayColor.B / 255f,
                hasInventoryDisplayColor.A / 255f);
            inventoryListItemPrefab.Name.color = color;
        }
    }
}