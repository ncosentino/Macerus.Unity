using Assets.Scripts.Unity.Threading;
using ProjectXyz.Game.Interface.GameObjects;
using ProjectXyz.Game.Interface.Mapping;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Maps
{
    public sealed class MapBehaviourStitcher : IMapBehaviourStitcher
    {
        private readonly IMapProvider _mapProvider;
        private readonly IExploreMapFormatter _exploreMapFormatter;
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IDispatcher _dispatcher;

        public MapBehaviourStitcher(
            IMapProvider mapProvider,
            IGameObjectManager gameObjectManager,
            IExploreMapFormatter exploreMapFormatter,
            IDispatcher dispatcher)
        {
            _mapProvider = mapProvider;
            _gameObjectManager = gameObjectManager;
            _exploreMapFormatter = exploreMapFormatter;
            _dispatcher = dispatcher;
        }

        public void Attach(GameObject mapGameObject)
        {
            Debug.Log($"Adding '{_mapProvider}' to '{mapGameObject}'...");
            
            var mapBehaviour = mapGameObject.AddComponent<MapBehaviour>();
            mapBehaviour.MapProvider = _mapProvider;
            mapBehaviour.ExploreMapFormatter = _exploreMapFormatter;
            mapBehaviour.GameObjectManager = _gameObjectManager;
            mapBehaviour.Dispatcher = _dispatcher;

            Debug.Log($"Added '{mapBehaviour}' to '{mapGameObject}'.");
        }
    }
}