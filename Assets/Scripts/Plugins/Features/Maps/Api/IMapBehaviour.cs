using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Interface.Mapping;

namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface IMapBehaviour : IReadonlyMapBehaviour
    {
        new IGameObjectManager GameObjectManager { get; set; }

        new IMapProvider MapProvider { get; set; }

        new IExploreMapFormatter ExploreMapFormatter { get; set; }
    }
}