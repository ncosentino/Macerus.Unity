using ProjectXyz.Game.Interface.Engine;

namespace Assets.Scripts.Scenes.Explore
{
    public interface IReadOnlyGameEngineUpdateBehaviour
    {
        IGameEngine GameEngine { get; }
    }
}