using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Api.GameObjects
{
    public interface IReadOnlyPrefabResourceBehavior : IBehavior
    {
        IIdentifier PrefabResourceId { get; }
    }
}