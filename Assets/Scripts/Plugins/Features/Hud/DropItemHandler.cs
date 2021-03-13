using System;

using Assets.Scripts.Plugins.Features.Hud.Api;

using Macerus.Plugins.Features.GameObjects.Containers.Api.LootDrops;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Assets.Scripts.Plugins.Features.Hud
{
    public sealed class DropItemHandler : IDropItemHandler
    {
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly ILootDropFactory _lootDropFactory;

        public DropItemHandler(
            IMapGameObjectManager mapGameObjectManager,
            ILootDropFactory lootFactory)
        {
            _mapGameObjectManager = mapGameObjectManager;
            _lootDropFactory = lootFactory;
        }

        public bool TryDropItem(
            double worldX,
            double worldY,
            IGameObject item,
            Func<bool> tryRemoveItemCallback)
        {
            // FIXME: check if we can drop the item here

            if (!tryRemoveItemCallback.Invoke())
            {
                return false;
            }

            var loot = _lootDropFactory.CreateLoot(
                worldX,
                worldY,
                false, // need to explicitly interact
                item);
            _mapGameObjectManager.MarkForAddition(loot);

            return true;
        }
    }
}
