using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common.Api
{
    public interface IHasGameObject : IReadOnlyHasGameObject
    {
        new IGameObject GameObject { get; set; }
    }
}