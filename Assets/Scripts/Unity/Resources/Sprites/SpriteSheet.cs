using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Unity.Resources.Sprites
{
    public sealed class SpriteSheet : ISpriteSheet
    {
        private readonly IReadOnlyDictionary<string, Sprite> _sheet;

        public SpriteSheet(IEnumerable<Sprite> sprites)
        {
            _sheet = sprites.ToDictionary(
                x => x.name,
                x => x,
                StringComparer.OrdinalIgnoreCase);
        }

        public bool TryGet(string key, out Sprite sprite)
        {
            return _sheet.TryGetValue(
                key,
                out sprite);
        }
    }
}