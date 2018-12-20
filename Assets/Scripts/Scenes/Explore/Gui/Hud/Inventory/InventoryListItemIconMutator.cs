using System.Linq;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources.Sprites;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using ProjectXyz.Api.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public sealed class InventoryListItemIconMutator : IInventoryListItemMutator
    {
        private readonly ISpriteLoader _spriteLoader;

        public InventoryListItemIconMutator(ISpriteLoader spriteLoader)
        {
            _spriteLoader = spriteLoader;
        }

        public void Mutate(
            GameObject inventoryListItemGameObject,
            IGameObject item)
        {
            var hasInventoryIcon = item
                .Get<IHasInventoryIcon>()
                .SingleOrDefault();
            if (hasInventoryIcon == null)
            {
                return;
            }

            var sprite = _spriteLoader.GetSpriteFromTexture2D(hasInventoryIcon.IconResource);
            inventoryListItemGameObject
                .GetRequiredComponentInChild<Image>("Icon")
                .sprite = sprite;
        }
    }
}