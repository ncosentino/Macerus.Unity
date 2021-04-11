using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Api.GameObjects
{
    public interface IDiscoverablePrefabStitcher : IPrefabStitcher
    {
        IIdentifier PrefabResourceId { get; }
    }
}