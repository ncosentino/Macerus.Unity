using System.IO;
using Assets.Scripts.Unity;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Shared.Framework.Collections;

namespace Assets.Scripts.Scenes.Explore.Maps
{
    public sealed class TilesetSpriteResourceResolver : ITilesetSpriteResourceResolver
    {
        private readonly IAssetPaths _assetPaths;
        private readonly ICache<string, string> _cache;

        public TilesetSpriteResourceResolver(IAssetPaths assetPaths)
        {
            _assetPaths = assetPaths;
            _cache = new Cache<string, string>(100);
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

            _cache.AddOrUpdate(tilesetResourcePath, relativeResourcePath);
            return relativeResourcePath;
        }
    }
}