using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Api.GameObjects
{
    public interface IDiscoverablePrefabSticher : IPrefabStitcher
    {
        IIdentifier PrefabResourceId { get; }
    }
}