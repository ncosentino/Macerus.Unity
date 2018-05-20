using Tiled.Net.Parsers;
using UnityEngine;

namespace Assets.Scripts.Maps
{
    public sealed class MapLoader : IMapLoader
    {
        private readonly IMapParser _mapParser;

        public MapLoader(IMapParser mapParser)
        {
            _mapParser = mapParser;
        }

        public void LoadMap(
            GameObject mapGameObject,
            string mapResourcePath)
        {
            var loadMapBehaviour = mapGameObject.AddComponent<LoadMapBehaviour>();
            loadMapBehaviour.MapResourcePath = @"C:\dev\nexus\products\Archive\__macerus-unity\Assets\Resources\Maps\swamp.tmx";
            loadMapBehaviour.MapParser = _mapParser;
        }
    }
}
