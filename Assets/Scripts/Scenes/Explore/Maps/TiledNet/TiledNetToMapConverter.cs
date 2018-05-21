using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjectXyz.Game.Core.Mapping;
using ProjectXyz.Game.Interface.Mapping;
using Tiled.Net.Dto.Tilesets;
using Tiled.Net.Maps;
using Tiled.Net.Tilesets;

namespace Assets.Scripts.Scenes.Explore.Maps.TiledNet
{
    public sealed class TiledNetToMapConverter : ITiledNetToMapConverter
    {
        private readonly ITilesetSpriteResourceResolver _tilesetSpriteResourceResolver;

        public TiledNetToMapConverter(ITilesetSpriteResourceResolver tilesetSpriteResourceResolver)
        {
            _tilesetSpriteResourceResolver = tilesetSpriteResourceResolver;
        }

        public IMap Convert(ITiledMap tiledMap)
        {
            var flipY = tiledMap.RenderOrder.IndexOf("-down", StringComparison.OrdinalIgnoreCase) != -1
                ? -1
                : 1;
            var flipX = tiledMap.RenderOrder.IndexOf("left-", StringComparison.OrdinalIgnoreCase) != -1
                ? -1
                : 1;
            var tilesetCache = new TilesetCache(tiledMap.Tilesets);

            var layers = tiledMap
                .Layers
                .Select(tiledMapLayer =>
                {
                    var mapLayerTiles = CreateTilesForLayer(
                        tilesetCache,
                        tiledMapLayer,
                        flipX,
                        flipY);
                    var mapLayer = new MapLayer(
                        tiledMapLayer.Name,
                        mapLayerTiles);
                    return mapLayer;
                });
            var map = new Map(layers);
            return map;
        }

        private IEnumerable<IMapTile> CreateTilesForLayer(
            ITilesetCache tilesetCache,
            Tiled.Net.Layers.IMapLayer tiledMapLayer,
            int flipX,
            int flipY)
        {
            for (var x = 0; x < tiledMapLayer.Width; x++)
            {
                for (var y = 0; y < tiledMapLayer.Height; y++)
                {
                    var tiledTile = tiledMapLayer.GetTile(x, y);
                    if (tiledTile.Gid == 0)
                    {
                        continue;
                    }

                    var tileset = tilesetCache.ForGid(tiledTile.Gid);
                    var tilesetTile = tileset.Tiles[tiledTile.Gid - 1];

                    var tilesetResourcePath = _tilesetSpriteResourceResolver.ResolveResourcePath(tileset.Images.Single().SourcePath);
                    var spriteResourceName = $"{Path.GetFileNameWithoutExtension(tilesetResourcePath)}_{tiledTile.Gid - tileset.FirstGid}";

                    var tileComponents = tilesetTile
                        .Properties
                        .Select(prop => (ITileComponent)new KeyValuePairTileComponent(
                            prop.Key,
                            prop.Value))
                        .Append(new TileResourceComponent(tilesetResourcePath, spriteResourceName));

                    var mapTile = new MapTile(
                        x * flipX,
                        y * flipY,
                        tileComponents);
                    yield return mapTile;
                }
            }
        }
    }
}