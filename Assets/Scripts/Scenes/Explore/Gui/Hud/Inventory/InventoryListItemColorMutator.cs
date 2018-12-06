using System.Linq;
using Assets.Scripts.Unity.GameObjects;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using ProjectXyz.Api.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public sealed class InventoryListItemColorMutator : IInventoryListItemMutator
    {
        public void Mutate(
            GameObject inventoryListItemGameObject,
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
            inventoryListItemGameObject
                .GetRequiredComponentInChild<Text>("Name")
                .color = color;
        }
    }
}