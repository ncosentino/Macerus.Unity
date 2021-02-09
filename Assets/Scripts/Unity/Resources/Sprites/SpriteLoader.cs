using System;
using System.Collections.Generic;

using ProjectXyz.Api.Framework;
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
            ICache<string, ISpriteSheet> spriteSheetCache) // FIXME: we want to cache by IIdentifier
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

        public Sprite GetSprite(IIdentifier spriteResourceId)
        {
            // FIXME: ID / string conversion
            var spriteResourceStr = spriteResourceId.ToString();
            var matchingSprite = _resourceLoader.Load<Sprite>(spriteResourceStr);
            return matchingSprite;
        }

        public Sprite SpriteFromMultiSprite(
            IIdentifier spriteSheetResourceId,
            IIdentifier spriteResourceId)
        {
            // FIXME: we want to cache by IIdentifier
            var spriteSheetResourceStr = spriteSheetResourceId.ToString();

            ISpriteSheet spriteSheet;
            if (!_spriteSheetCache.TryGetValue(
                spriteSheetResourceStr,
                out spriteSheet))
            {
                IReadOnlyCollection<Sprite> matchingSprites;
                try
                {
                    matchingSprites = _resourceLoader.LoadAll<Sprite>(spriteSheetResourceStr);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(
                        $"No sprite sheet found with ID '{spriteSheetResourceId}'.",
                        ex);
                }

                spriteSheet = new SpriteSheet(matchingSprites);
                _spriteSheetCache.AddOrUpdate(spriteSheetResourceStr, spriteSheet);
            }

            Sprite sprite;
            if (!spriteSheet.TryGet(spriteResourceId, out sprite))
            {
                throw new InvalidOperationException(
                    $"No sprite with ID '{spriteResourceId}' in sprite sheet " +
                    $"'{spriteSheetResourceId}'.");
            }

            return sprite;
        }
    }
}
