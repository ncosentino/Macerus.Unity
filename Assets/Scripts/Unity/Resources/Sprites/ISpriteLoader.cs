using UnityEngine;

namespace Assets.Scripts.Unity.Resources.Sprites
{
    public interface ISpriteLoader
    {
        Sprite GetSpriteFromTexture2D(string texture2DResource);

        Sprite SpriteFromMultiSprite(
            string spriteSheetResource,
            string spriteName);

        Sprite GetSprite(string spriteResource);
    }
}