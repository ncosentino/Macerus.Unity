using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Unity;
using Tiled.Net.Tilesets;

namespace Assets.Scripts.Maps
{
    public sealed class TilesetSpriteResourceResolver : ITilesetSpriteResourceResolver
    {
        private readonly IAssetPaths _assetPaths;
        private readonly Dictionary<ITileset, string> _cache;

        public TilesetSpriteResourceResolver(IAssetPaths assetPaths)
        {
            _assetPaths = assetPaths;
            _cache = new Dictionary<ITileset, string>();
        }

        public string ResolveResourcePath(ITileset tileset)
        {
            string cached;
            if (_cache.TryGetValue(tileset, out cached))
            {
                return cached;
            }

            var tilesetSourceImagePath = Path.GetDirectoryName(tileset.Images.Single().SourcePath);
            var fullResourcePath = Path.GetFullPath(Path.Combine(_assetPaths.MapsRoot, tilesetSourceImagePath));
            var relativeResourcePath = fullResourcePath.Substring(_assetPaths.ResourcesRoot.Length + 1);

            _cache[tileset] = relativeResourcePath;
            return relativeResourcePath;
        }
    }
}