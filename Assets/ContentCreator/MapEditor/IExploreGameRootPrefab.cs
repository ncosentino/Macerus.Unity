using Assets.Scripts.Plugins.Features.Maps;
using Assets.Scripts.Unity.Resources.Prefabs;

namespace Assets.ContentCreator.MapEditor
{
    public interface IExploreGameRootPrefab : IPrefab
    {
        IMapPrefab MapPrefab { get; }
    }
}
