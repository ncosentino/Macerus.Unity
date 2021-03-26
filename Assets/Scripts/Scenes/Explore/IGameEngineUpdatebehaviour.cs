using ProjectXyz.Game.Interface.Engine;

namespace Assets.Scripts.Scenes.Explore
{
    public interface IGameEngineUpdateBehaviour : IReadOnlyGameEngineUpdateBehaviour
    {
        new IGameEngine GameEngine { get; set; }
    }
}