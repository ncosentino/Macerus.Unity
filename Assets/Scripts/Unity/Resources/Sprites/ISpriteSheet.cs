using UnityEngine;

namespace Assets.Scripts.Unity.Resources.Sprites
{
    public interface ISpriteSheet
    {
        bool TryGet(
            string key,
            out Sprite sprite);
    }
}