using ProjectXyz.Game.Interface.Engine;

namespace Assets.ContentCreator.MapEditor
{
    public interface IReadOnlyGameEngineUpdateBehaviour
    {
        IGameEngine GameEngine { get; }
    }
}