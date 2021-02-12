using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Api.GameObjects
{
    public interface IPrefabResourceBehavior : IReadOnlyPrefabResourceBehavior
    {
        new IIdentifier PrefabResourceId { get; set; }
    }
}