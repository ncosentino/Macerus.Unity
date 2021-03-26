using ProjectXyz.Game.Interface.Engine;

namespace Assets.Scripts.Scenes.Explore
{
    public interface IGameEngineUpdateBehaviour : IReadOnlyGameEngineUpdateBehaviour
    {
        new IAsyncGameEngine GameEngine { get; set; }
    }
}