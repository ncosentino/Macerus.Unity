using System.IO;
using ProjectXyz.Game.Interface.Mapping;
using Tiled.Net.Maps;
using Tiled.Net.Parsers;

namespace Assets.Scripts.Scenes.Explore.Maps.TiledNet
{
    public sealed class TiledNetMapRepository : IMapRepository
    {
        private readonly IMapParser _mapParser;
        private readonly ITiledNetToMapConverter _tiledNetToMapConverter;
        private readonly IMapResourceIdConverter _mapResourceIdConverter;

        public TiledNetMapRepository(
            IMapParser mapParser,
            ITiledNetToMapConverter tiledNetToMapConverter,
            IMapResourceIdConverter mapResourceIdConverter)
        {
            _mapParser = mapParser;
            _tiledNetToMapConverter = tiledNetToMapConverter;
            _mapResourceIdConverter = mapResourceIdConverter;
        }

        public IMap LoadMap(string mapResourceId)
        {
            var mapResourcePath = _mapResourceIdConverter.Convert(mapResourceId);

            ITiledMap tiledMap;
            using (var mapResourceStream = new FileStream(
                mapResourcePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read))
            {
                tiledMap = _mapParser.ParseMap(mapResourceStream);
            }

            var map = _tiledNetToMapConverter.Convert(tiledMap);
            return map;
        }
    }
}