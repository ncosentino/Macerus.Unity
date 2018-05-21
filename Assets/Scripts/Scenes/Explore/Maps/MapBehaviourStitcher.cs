using ProjectXyz.Game.Interface.Mapping;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Maps
{
    public sealed class MapBehaviourStitcher : IMapBehaviourStitcher
    {
        private readonly IMapProvider _mapProvider;
        private readonly IExploreMapFormatter _exploreMapFormatter;

        public MapBehaviourStitcher(
            IMapProvider mapProvider,
            IExploreMapFormatter exploreMapFormatter)
        {
            _mapProvider = mapProvider;
            _exploreMapFormatter = exploreMapFormatter;
        }

        public void Attach(GameObject mapGameObject)
        {
            Debug.Log($"Adding '{_mapProvider}' to '{mapGameObject}'...");
            
            var mapBehaviour = mapGameObject.AddComponent<MapBehaviour>();
            mapBehaviour.MapProvider = _mapProvider;
            mapBehaviour.ExploreMapFormatter = _exploreMapFormatter;

            Debug.Log($"Added '{mapBehaviour}' to '{mapGameObject}'.");
        }
    }
}