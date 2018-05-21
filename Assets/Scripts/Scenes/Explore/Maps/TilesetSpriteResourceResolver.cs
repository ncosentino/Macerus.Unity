using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Unity;

namespace Assets.Scripts.Scenes.Explore.Maps
{
    public sealed class TilesetSpriteResourceResolver : ITilesetSpriteResourceResolver
    {
        private readonly IAssetPaths _assetPaths;
        private readonly Dictionary<string, string> _cache;

        public TilesetSpriteResourceResolver(IAssetPaths assetPaths)
        {
            _assetPaths = assetPaths;
            _cache = new Dictionary<string, string>();
        }

        public string ResolveResourcePath(string tilesetResourcePath)
        {
            string cached;
            if (_cache.TryGetValue(tilesetResourcePath, out cached))
            {
                return cached;
            }

            var tilesetSourceImagePath = Path.GetDirectoryName(tilesetResourcePath);
            var fullResourcePath = Path.GetFullPath(Path.Combine(_assetPaths.MapsRoot, tilesetSourceImagePath));
            var relativeResourcePath = fullResourcePath.Substring(_assetPaths.ResourcesRoot.Length + 1);

            _cache[tilesetResourcePath] = relativeResourcePath;
            return relativeResourcePath;
        }
    }
}