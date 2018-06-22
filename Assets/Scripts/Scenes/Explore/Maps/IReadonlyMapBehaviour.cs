using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Interface.Mapping;

namespace Assets.Scripts.Scenes.Explore.Maps
{
    public interface IReadonlyMapBehaviour
    {
        IGameObjectManager GameObjectManager { get; }

        IMapProvider MapProvider { get; }

        IExploreMapFormatter ExploreMapFormatter { get; }
    }
}