using System;
using ProjectXyz.Api.Framework.Collections;
using UnityEngine;

namespace Assets.Scripts.Unity.Resources.Sprites
{
    public sealed class SpriteLoader : ISpriteLoader
    {
        private readonly ICache<string, ISpriteSheet> _spriteSheetCache;

        public SpriteLoader(ICache<string, ISpriteSheet> spriteSheetCache)
        {
            _spriteSheetCache = spriteSheetCache;
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
                // TODO: use an IResourceLoader that has a LoadAll<> signature...
                var matchingSprites = UnityEngine.Resources.LoadAll<Sprite>(spriteSheetResource);
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
