using System;
using UnityEngine;

namespace Assets.Scripts.Unity.Resources.Sprites
{
    public sealed class SpriteLoader : ISpriteLoader
    {
        private readonly ISpriteSheetCache _spriteSheetCache;

        public SpriteLoader(ISpriteSheetCache spriteSheetCache)
        {
            _spriteSheetCache = spriteSheetCache;
        }

        public Sprite SpriteFromMultiSprite(
            string spriteSheetResource,
            string spriteName)
        {
            ISpriteSheet spriteSheet;
            if (!_spriteSheetCache.TryGet(
                spriteSheetResource,
                out spriteSheet))
            {
                var matchingSprites = UnityEngine.Resources.LoadAll<Sprite>(spriteSheetResource);
                spriteSheet = new SpriteSheet(matchingSprites);
                _spriteSheetCache.Add(spriteSheetResource, spriteSheet);
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
