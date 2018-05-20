using System;
using System.IO;
using Assets.Scripts.Unity.Resources.Sprites;
using Tiled.Net.Dto.Tilesets;
using Tiled.Net.Maps;
using Tiled.Net.Parsers;
using UnityEngine;

namespace Assets.Scripts.Maps
{
    public sealed class LoadMapBehaviour : MonoBehaviour
    {
        public IMapParser MapParser { get; set; }

        public ITileLoader TileLoader { get; set; }

        public string MapResourcePath { get; set; }

        private void Start()
        {
            if (MapParser == null)
            {
                throw new InvalidOperationException($"{nameof(MapParser)} was not set.");
            }

            if (TileLoader == null)
            {
                throw new InvalidOperationException($"{nameof(TileLoader)} was not set.");
            }

            if (string.IsNullOrWhiteSpace(MapResourcePath))
            {
                throw new InvalidOperationException($"{nameof(MapResourcePath)} was null or whitespace.");
            }

            Debug.Log($"Loading map '{MapResourcePath}'...");

            ITiledMap map;
            using (var mapResourceStream = File.OpenRead(MapResourcePath))
            {
                map = MapParser.ParseMap(mapResourceStream);
            }

            var tilesetCache = new TilesetCache(map.Tilesets);
            var flipY = map.RenderOrder.IndexOf("-down", StringComparison.OrdinalIgnoreCase) != -1
                ? -1
                : 1;
            var flipX = map.RenderOrder.IndexOf("left-", StringComparison.OrdinalIgnoreCase) != -1
                ? -1
                : 1;

            var parentMapObjectTransform = this.gameObject.transform;

            int z = 0;
            foreach (var mapLayer in map.Layers)
            {
                var mapLayerObject = new GameObject($"{mapLayer.Name} ({mapLayer.Width}x{mapLayer.Height})");
                mapLayerObject.transform.parent = parentMapObjectTransform;

                for (int x = 0; x < mapLayer.Width; x++)
                {
                    for (int y = 0; y < mapLayer.Height; y++)
                    {
                        var tile = mapLayer.GetTile(x, y);
                        if (tile.Gid == 0)
                        {
                            continue;
                        }

                        var tileset = tilesetCache.ForGid(tile.Gid);
                        var tilesetTile = tileset.Tiles[tile.Gid - 1];

                        var tileObject = TileLoader.CreateTile(
                            tileset,
                            tile,
                            x * flipX,
                            y * flipY,
                            z);
                        tileObject.transform.parent = mapLayerObject.transform;
                    }
                }

                z++;
            }

            Debug.Log($"Loaded map '{MapResourcePath}'.");
            Destroy(this);
        }
    }
}
