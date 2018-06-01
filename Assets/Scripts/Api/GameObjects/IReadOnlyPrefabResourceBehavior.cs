using ProjectXyz.Api.Behaviors;

namespace Assets.Scripts.Api.GameObjects
{
    public interface IReadOnlyPrefabResourceBehavior : IBehavior
    {
        string PrefabResourceId { get; }
    }
}