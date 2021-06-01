using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.Resources.Prefabs;

namespace Assets.Scripts.Scenes.Explore
{
    public interface IExploreGameRootPrefab : IPrefab
    {
        IMapPrefab MapPrefab { get; }
    }
}
