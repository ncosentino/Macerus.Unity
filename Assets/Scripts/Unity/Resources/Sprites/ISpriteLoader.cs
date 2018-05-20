using UnityEngine;

namespace Assets.Scripts.Unity.Resources.Sprites
{
    public interface ISpriteLoader
    {
        Sprite SpriteFromMultiSprite(
            string spriteSheetResource,
            string spriteName);
    }
}