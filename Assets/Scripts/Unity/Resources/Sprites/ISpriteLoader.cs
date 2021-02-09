using ProjectXyz.Api.Framework;

using UnityEngine;

namespace Assets.Scripts.Unity.Resources.Sprites
{
    public interface ISpriteLoader
    {
        Sprite GetSpriteFromTexture2D(string texture2DResource);

        Sprite SpriteFromMultiSprite(
            IIdentifier spriteSheetResourceId,
            IIdentifier spriteResourceId);

        Sprite GetSprite(IIdentifier spriteResourceId);
    }
}