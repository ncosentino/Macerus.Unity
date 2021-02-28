using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface IReadonlyMapBehaviour
    {
        IGameObjectManager GameObjectManager { get; }

        IMapProvider MapProvider { get; }

        IMapFormatter ExploreMapFormatter { get; }

        IWeatherManager WeatherManager { get; }

        IWeatherTableRepositoryFacade WeatherTableRepositoryFacade { get; }

        IFilterContextFactory FilterContextFactory { get; }
    }
}