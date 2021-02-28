using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public sealed class MapPrefabFactory : IMapPrefabFactory
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly IMapBehaviourStitcher _mapBehaviourStitcher;

        public MapPrefabFactory(
            IPrefabCreator prefabCreator,
            IMapBehaviourStitcher mapBehaviourStitcher)
        {
            _prefabCreator = prefabCreator;
            _mapBehaviourStitcher = mapBehaviourStitcher;
        }

        public GameObject CreateMap()
        {
            var mapObject = _prefabCreator.Create<GameObject>("Mapping/Prefabs/Map");
            mapObject.name = "Map";
            _mapBehaviourStitcher.Attach(mapObject);
            return mapObject;
        }
    }
}
