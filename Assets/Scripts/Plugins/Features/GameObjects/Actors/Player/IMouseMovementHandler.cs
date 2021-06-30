using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IMouseMovementHandler
    {
        Task HandleMouseMovementAsync(IGameObject actor);
    }
}
