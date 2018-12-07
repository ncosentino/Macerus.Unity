using System;
using ProjectXyz.Api.Framework.Collections;
using UnityEngine;

namespace Assets.Scripts.Unity.Resources.Sprites
{
    public sealed class SpriteLoader : ISpriteLoader
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly ICache<string, ISpriteSheet> _spriteSheetCache;

        public SpriteLoader(
            IResourceLoader resourceLoader,
            ICache<string, ISpriteSheet> spriteSheetCache)
        {
            _resourceLoader = resourceLoader;
            _spriteSheetCache = spriteSheetCache;
        }

        public Sprite GetSpriteFromTexture2D(string texture2DResource)
        {
            var texture = _resourceLoader.Load<Texture2D>(texture2DResource);
            var sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                Vector2.zero);
            return sprite;
        }

        public Sprite GetSprite(string spriteResource)
        {
            var matchingSprite = _resourceLoader.Load<Sprite>(spriteResource);
            return matchingSprite;
        }

        public Sprite SpriteFromMultiSprite(
            string spriteSheetResource,
            string spriteName)
        {
            ISpriteSheet spriteSheet;
            if (!_spriteSheetCache.TryGetValue(
                spriteSheetResource,
                out spriteSheet))
            {
                var matchingSprites = _resourceLoader.LoadAll<Sprite>(spriteSheetResource);
                spriteSheet = new SpriteSheet(matchingSprites);
                _spriteSheetCache.AddOrUpdate(spriteSheetResource, spriteSheet);
            }

            Sprite sprite;
            if (!spriteSheet.TryGet(spriteName, out sprite))
            {
                throw new InvalidOperationException($"No sprite named '{spriteName}' in sprite sheet '{spriteSheetResource}'.");
            }

            return sprite;
        }
    }
}
