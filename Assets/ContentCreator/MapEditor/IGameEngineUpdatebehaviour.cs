using ProjectXyz.Game.Interface.Engine;

namespace Assets.ContentCreator.MapEditor
{
    public interface IGameEngineUpdateBehaviour : IReadOnlyGameEngineUpdateBehaviour
    {
        new IGameEngine GameEngine { get; set; }
    }
}