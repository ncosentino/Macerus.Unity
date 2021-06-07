
using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IKeyboardMovementHandler
    {
        void HandleKeyboardMovement(IGameObject actor);
    }
}
