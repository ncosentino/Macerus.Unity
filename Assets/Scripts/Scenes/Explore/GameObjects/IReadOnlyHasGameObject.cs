using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IReadOnlyHasGameObject
    {
        IGameObject GameObject { get; }
    }
}