using System.Linq;
using ProjectXyz.Game.Interface.Mapping;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Maps
{
    public sealed class ExploreMapFormatter : IExploreMapFormatter
    {
        private readonly ITileLoader _tileLoader;

        public ExploreMapFormatter(ITileLoader tileLoader)
        {
            _tileLoader = tileLoader;
        }

        public void FormatMap(
            GameObject mapObject,
            IMap map)
        {
            Debug.Log($"Formatting map object '{mapObject}' for '{map}'...");

            var parentMapObjectTransform = mapObject.transform;

            foreach (Transform child in parentMapObjectTransform)
            {
                Object.Destroy(child.gameObject);
            }

            int z = 0;
            foreach (var mapLayer in map.Layers)
            {
                var mapLayerObject = new GameObject($"{mapLayer.Name}");
                mapLayerObject.transform.parent = parentMapObjectTransform;

                foreach (var tile in mapLayer.Tiles)
                {
                    var tileResource = (ITileResourceComponent)tile.Components.First(x => x is ITileResourceComponent);
                    var tileObject = _tileLoader.CreateTile(
                        tile.X,
                        tile.Y,
                        z,
                        tileResource.TilesetResourcePath,
                        tileResource.SpriteResourceName);
                    tileObject.transform.parent = mapLayerObject.transform;
                }
                
                z++;
            }

            Debug.Log($"Formatted map object '{mapObject}' for '{map}'.");
        }
    }
}