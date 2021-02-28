using Assets.Scripts.Plugins.Features.Maps.Api;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.Weather.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class MapBehaviourStitcher : IMapBehaviourStitcher
    {
        private readonly IMapProvider _mapProvider;
        private readonly IMapFormatter _exploreMapFormatter;
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly IWeatherManager _weatherManager;
        private readonly IWeatherTableRepositoryFacade _weatherTableRepositoryFacade;
        private readonly IGameObjectManager _gameObjectManager;
        private readonly ILogger _logger;

        public MapBehaviourStitcher(
            IMapProvider mapProvider,
            IGameObjectManager gameObjectManager,
            IMapFormatter exploreMapFormatter,
            IFilterContextFactory filterContextFactory,
            IWeatherManager weatherManager,
            IWeatherTableRepositoryFacade weatherTableRepositoryFacade,
            ILogger logger)
        {
            _mapProvider = mapProvider;
            _gameObjectManager = gameObjectManager;
            _exploreMapFormatter = exploreMapFormatter;
            _filterContextFactory = filterContextFactory;
            _weatherManager = weatherManager;
            _weatherTableRepositoryFacade = weatherTableRepositoryFacade;
            _logger = logger;
        }

        public void Attach(GameObject mapGameObject)
        {
            _logger.Debug($"Adding '{_mapProvider}' to '{mapGameObject}'...");
            
            var mapBehaviour = mapGameObject.AddComponent<MapBehaviour>();
            mapBehaviour.MapProvider = _mapProvider;
            mapBehaviour.ExploreMapFormatter = _exploreMapFormatter;
            mapBehaviour.GameObjectManager = _gameObjectManager;
            mapBehaviour.FilterContextFactory = _filterContextFactory;
            mapBehaviour.WeatherManager = _weatherManager;
            mapBehaviour.WeatherTableRepositoryFacade = _weatherTableRepositoryFacade;

            _logger.Debug($"Added '{mapBehaviour}' to '{mapGameObject}'.");
        }
    }
}