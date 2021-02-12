using System.Linq;
using Assets.Scripts.Unity.Resources.Sprites;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class InventoryListItemIconMutator : IInventoryListItemMutator
    {
        private readonly ISpriteLoader _spriteLoader;

        public InventoryListItemIconMutator(ISpriteLoader spriteLoader)
        {
            _spriteLoader = spriteLoader;
        }

        public void Mutate(
            IInventoryListItemPrefab inventoryListItemPrefab,
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
            inventoryListItemPrefab.Icon.sprite = sprite;
        }
    }
}