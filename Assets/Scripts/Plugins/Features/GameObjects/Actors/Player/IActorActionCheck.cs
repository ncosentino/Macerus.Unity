using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IActorActionCheck
    {
        bool CanAct(IGameObject actor);
    }
}