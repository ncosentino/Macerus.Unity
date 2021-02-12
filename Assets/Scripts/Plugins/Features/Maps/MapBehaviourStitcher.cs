using Assets.Scripts.Plugins.Features.Maps.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Interface.Mapping;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class MapBehaviourStitcher : IMapBehaviourStitcher
    {
        private readonly IMapProvider _mapProvider;
        private readonly IExploreMapFormatter _exploreMapFormatter;
        private readonly IGameObjectManager _gameObjectManager;
        private readonly ILogger _logger;

        public MapBehaviourStitcher(
            IMapProvider mapProvider,
            IGameObjectManager gameObjectManager,
            IExploreMapFormatter exploreMapFormatter,
            ILogger logger)
        {
            _mapProvider = mapProvider;
            _gameObjectManager = gameObjectManager;
            _exploreMapFormatter = exploreMapFormatter;
            _logger = logger;
        }

        public void Attach(GameObject mapGameObject)
        {
            _logger.Debug($"Adding '{_mapProvider}' to '{mapGameObject}'...");
            
            var mapBehaviour = mapGameObject.AddComponent<MapBehaviour>();
            mapBehaviour.MapProvider = _mapProvider;
            mapBehaviour.ExploreMapFormatter = _exploreMapFormatter;
            mapBehaviour.GameObjectManager = _gameObjectManager;

            _logger.Debug($"Added '{mapBehaviour}' to '{mapGameObject}'.");
        }
    }
}