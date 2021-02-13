using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Interface.Mapping;

namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface IReadonlyMapBehaviour
    {
        IGameObjectManager GameObjectManager { get; }

        IMapProvider MapProvider { get; }

        IMapFormatter ExploreMapFormatter { get; }
    }
}