using Assets.Scripts.Plugins.Features.Maps;
using Assets.Scripts.Unity.Resources.Prefabs;

namespace Assets.Scripts.Scenes.Explore
{
    public interface IExploreGameRootPrefab : IPrefab
    {
        IMapPrefab MapPrefab { get; }
    }
}
