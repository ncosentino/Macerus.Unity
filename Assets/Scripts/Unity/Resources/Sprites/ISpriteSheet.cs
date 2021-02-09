using ProjectXyz.Api.Framework;

using UnityEngine;

namespace Assets.Scripts.Unity.Resources.Sprites
{
    public interface ISpriteSheet
    {
        bool TryGet(
            IIdentifier spriteResourceId,
            out Sprite sprite);
    }
}