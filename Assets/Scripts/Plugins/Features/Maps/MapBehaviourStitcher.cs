using Assets.ContentCreator.MapEditor;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.Threading;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public sealed class MapBehaviourStitcher : IMapBehaviourStitcher
    {
        private readonly IMapProvider _mapProvider;
        private readonly IMapFormatter _exploreMapFormatter;
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly IWeatherManager _weatherManager;
        private readonly IWeatherTableRepositoryFacade _weatherTableRepositoryFacade;
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;
        private readonly IDispatcher _dispatcher;
        private readonly ISceneToMapConverter _sceneToMapConverter;

        public MapBehaviourStitcher(
            IMapProvider mapProvider,
            IReadOnlyMapGameObjectManager mapGameObjectManager,
            IMapFormatter exploreMapFormatter,
            IFilterContextFactory filterContextFactory,
            IWeatherManager weatherManager,
            IWeatherTableRepositoryFacade weatherTableRepositoryFacade,
            IDispatcher dispatcher,
            ISceneToMapConverter sceneToMapConverter)
        {
            _mapProvider = mapProvider;
            _mapGameObjectManager = mapGameObjectManager;
            _exploreMapFormatter = exploreMapFormatter;
            _filterContextFactory = filterContextFactory;
            _weatherManager = weatherManager;
            _weatherTableRepositoryFacade = weatherTableRepositoryFacade;
            _dispatcher = dispatcher;
            _sceneToMapConverter = sceneToMapConverter;
        }

        public void Attach(IMapPrefab mapPrefab)
        {           
            var mapBehaviour = mapPrefab.GameObject.AddComponent<MapBehaviour>();
            mapBehaviour.MapProvider = _mapProvider;
            mapBehaviour.MapFormatter = _exploreMapFormatter;
            mapBehaviour.MapGameObjectManager = _mapGameObjectManager;
            mapBehaviour.FilterContextFactory = _filterContextFactory;
            mapBehaviour.WeatherManager = _weatherManager;
            mapBehaviour.WeatherTableRepositoryFacade = _weatherTableRepositoryFacade;
            mapBehaviour.Dispatcher = _dispatcher;
            mapBehaviour.MapPrefab = mapPrefab;
            mapBehaviour.SceneToMapConverter = _sceneToMapConverter;
        }
    }
}