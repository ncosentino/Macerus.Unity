using System.Linq;
using Assets.Scripts.Unity.GameObjects;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using ProjectXyz.Api.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public sealed class InventoryListItemNameMutator : IInventoryListItemMutator
    {
        public void Mutate(
            GameObject inventoryListItemGameObject,
            IGameObject item)
        {
            var hasInventoryDisplayName = item
                .Get<IHasInventoryDisplayName>()
                .SingleOrDefault();
            if (hasInventoryDisplayName == null)
            {
                return;
            }

            inventoryListItemGameObject
                .GetRequiredComponentInChild<Text>("Name")
                .text = hasInventoryDisplayName.DisplayName;
        }
    }
}