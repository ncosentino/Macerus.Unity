using System;
using System.IO;
using Tiled.Net.Maps;
using Tiled.Net.Parsers;
using UnityEngine;

namespace Assets.Scripts.Maps
{
    public sealed class LoadMapBehaviour : MonoBehaviour
    {
        public IMapParser MapParser { get; set; }

        public string MapResourcePath { get; set; }

        private void Start()
        {
            if (MapParser == null)
            {
                throw new InvalidOperationException("The map parser was not set.");
            }

            if (string.IsNullOrWhiteSpace(MapResourcePath))
            {
                throw new InvalidOperationException("The map resource path was not set.");
            }

            Debug.Log($"Loading map '{MapResourcePath}'...");

            ITiledMap map;
            using (var mapResourceStream = File.OpenRead(MapResourcePath))
            {
                map = MapParser.ParseMap(mapResourceStream);
            }

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
                        var tileObject = Instantiate(
                            Resources.Load("Maps/Prefabs/Tile", typeof(GameObject)),
                            mapLayerObject.transform) as GameObject;
                        tileObject.name = $"Tile ({x}x{y})";
                        tileObject.transform.position = new Vector3(x, y, z);

                        var renderer = tileObject.GetComponent<SpriteRenderer>();
                        renderer.sprite = Instantiate(Resources.Load("Maps/Tilesets/Test/sprite", typeof(Sprite))) as Sprite;
                    }
                }

                z++;
            }

            Debug.Log($"Loaded map '{MapResourcePath}'.");
            Destroy(this);
        }
    }
}
