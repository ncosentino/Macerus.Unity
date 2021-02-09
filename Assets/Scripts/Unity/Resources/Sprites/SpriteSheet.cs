using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Unity.Resources.Sprites
{
    public sealed class SpriteSheet : ISpriteSheet
    {
        // FIXME: should cache by id...
        private readonly IReadOnlyDictionary<string, Sprite> _sheet;

        public SpriteSheet(IEnumerable<Sprite> sprites)
        {
            _sheet = sprites.ToDictionary(
                x => x.name,
                x => x,
                StringComparer.OrdinalIgnoreCase); // NOTE: switching this to IDs will become case sensitive!
        }

        public bool TryGet(IIdentifier spriteResourceId, out Sprite sprite)
        {
            return _sheet.TryGetValue(
                spriteResourceId.ToString(), // FIXME: should cache by id...
                out sprite);
        }
    }
}