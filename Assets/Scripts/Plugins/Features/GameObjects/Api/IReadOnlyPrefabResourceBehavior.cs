using ProjectXyz.Api.Behaviors;

namespace Assets.Scripts.Plugins.Features.GameObjects.Api
{
    public interface IReadOnlyPrefabResourceBehavior : IBehavior
    {
        string PrefabResourceId { get; }
    }
}