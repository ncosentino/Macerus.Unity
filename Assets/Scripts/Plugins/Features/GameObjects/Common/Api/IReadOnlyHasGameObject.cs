using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common.Api
{
    public interface IReadOnlyHasGameObject
    {
        IGameObject GameObject { get; }
    }
}