using Assets.Scripts.Unity.Threading;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface IReadonlyMapBehaviour
    {
        IReadOnlyMapGameObjectManager MapGameObjectManager { get; }

        IMapProvider MapProvider { get; }

        IMapFormatter MapFormatter { get; }

        IWeatherManager WeatherManager { get; }

        IWeatherTableRepositoryFacade WeatherTableRepositoryFacade { get; }

        IFilterContextFactory FilterContextFactory { get; }

        IDispatcher Dispatcher { get; }
    }
}