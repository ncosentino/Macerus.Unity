using System;

using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.Hud.Api
{
    public interface IDropItemHandler
    {
        bool TryDropItem(
            IGameObject item,
            Func<bool> tryRemoveItemCallback);
    }
}
