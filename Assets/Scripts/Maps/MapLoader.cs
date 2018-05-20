using Tiled.Net.Parsers;
using UnityEngine;

namespace Assets.Scripts.Maps
{
    public sealed class MapLoader : IMapLoader
    {
        private readonly IMapParser _mapParser;
        private readonly ITileLoader _tileLoader;

        public MapLoader(
            IMapParser mapParser,
            ITileLoader tileLoader)
        {
            _mapParser = mapParser;
            _tileLoader = tileLoader;
        }

        public void LoadMap(
            GameObject mapGameObject,
            string mapResourcePath)
        {
            var loadMapBehaviour = mapGameObject.AddComponent<LoadMapBehaviour>();
            loadMapBehaviour.MapResourcePath = mapResourcePath;
            loadMapBehaviour.TileLoader = _tileLoader;
            loadMapBehaviour.MapParser = _mapParser;
        }
    }
}
