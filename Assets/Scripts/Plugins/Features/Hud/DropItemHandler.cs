using System;

using Assets.Scripts.Plugins.Features.Hud.Api;

using Macerus.Plugins.Features.GameObjects.Containers.Api.LootDrops;

using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.Hud
{
    public sealed class DropItemHandler : IDropItemHandler
    {
        private readonly IMutableGameObjectManager _gameObjectManager;
        private readonly ILootDropFactory _lootDropFactory;

        public DropItemHandler(
            IMutableGameObjectManager gameObjectManager,
            ILootDropFactory lootFactory)
        {
            _gameObjectManager = gameObjectManager;
            _lootDropFactory = lootFactory;
        }

        public bool TryDropItem(
            IGameObject item,
            Func<bool> tryRemoveItemCallback)
        {
            // FIXME: check if we can drop the item here

            if (!tryRemoveItemCallback.Invoke())
            {
                return false;
            }

            // FIXME: get the placement!
            var loot = _lootDropFactory.CreateLoot(
                39,
                -16,
                item);
            _gameObjectManager.MarkForAddition(loot);

            return true;
        }
    }
}
