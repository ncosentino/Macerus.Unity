using System.IO;
using Assets.Scripts.Unity.Resources;
using ProjectXyz.Game.Interface.Mapping;
using Tiled.Net.Maps;
using Tiled.Net.Parsers;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Maps.TiledNet
{
    public sealed class TiledNetMapRepository : IMapRepository
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly IMapParser _mapParser;
        private readonly ITiledNetToMapConverter _tiledNetToMapConverter;
        private readonly IMapResourceIdConverter _mapResourceIdConverter;

        public TiledNetMapRepository(
            IResourceLoader resourceLoader,
            IMapParser mapParser,
            ITiledNetToMapConverter tiledNetToMapConverter,
            IMapResourceIdConverter mapResourceIdConverter)
        {
            _resourceLoader = resourceLoader;
            _mapParser = mapParser;
            _tiledNetToMapConverter = tiledNetToMapConverter;
            _mapResourceIdConverter = mapResourceIdConverter;
        }

        public IMap LoadMap(string mapResourceId)
        {
            var mapResourcePath = _mapResourceIdConverter.Convert(mapResourceId);
            
            ITiledMap tiledMap;
            using (var mapResourceStream = _resourceLoader.LoadStream(mapResourcePath))
            {
                tiledMap = _mapParser.ParseMap(mapResourceStream);
            }

            var map = _tiledNetToMapConverter.Convert(tiledMap);
            return map;
        }
    }
}