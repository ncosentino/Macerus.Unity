using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IHasGameObject : IReadOnlyHasGameObject
    {
        new IGameObject GameObject { get; set; }
    }
}