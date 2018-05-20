using System;
using System.Collections.Specialized;

namespace Assets.Scripts.Unity.Resources.Sprites
{
    public sealed class SpriteSheetCache : ISpriteSheetCache
    {
        private readonly OrderedDictionary _cache;
        private readonly int _limit;

        public SpriteSheetCache(int limit)
        {
            if (limit < 1)
            {
                throw new ArgumentException(
                    $"{nameof(limit)} must be >= 1.",
                    nameof(limit));
            }

            _cache = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
            _limit = limit;
        }

        public void Add(string key, ISpriteSheet spriteSheet)
        {
            while (_cache.Count >= _limit - 1)
            {
                _cache.RemoveAt(0);
            }

            _cache.Add(key, spriteSheet);
        }

        public bool TryGet(string key, out ISpriteSheet spriteSheet)
        {
            if (_cache.Contains(key))
            {
                spriteSheet = (ISpriteSheet)_cache[key];
                return true;
            }

            spriteSheet = null;
            return false;
        }
    }
}