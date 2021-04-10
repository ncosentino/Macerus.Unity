using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.Resources.Sprites;

using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Shared.Framework;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public sealed class TileLoader : ITileLoader
    {
        private readonly ISpriteLoader _spriteLoader;
        private readonly ICache<string, Tile> _tileCache;

        public TileLoader(
            ISpriteLoader spriteLoader,
            ICache<string, Tile> tileCache)
        {;
            _spriteLoader = spriteLoader;
            _tileCache = tileCache;
        }

        public Tile LoadTile(
            string relativeResourcePath,
            string spriteResourceName)
        {
            var cacheKey = relativeResourcePath + spriteResourceName;
            if (_tileCache.TryGetValue(cacheKey, out var cachedTile))
            {
                return cachedTile;
            }

            // FIXME: resource path/name to ID conversion
            var sprite = _spriteLoader.SpriteFromMultiSprite(
                new StringIdentifier(relativeResourcePath),
                new StringIdentifier(spriteResourceName));
            var unityTile = ScriptableObject.CreateInstance<Tile>();
            unityTile.sprite = sprite;

            _tileCache.AddOrUpdate(cacheKey, unityTile);
            return unityTile;
        }
    }
}