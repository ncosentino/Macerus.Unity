using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Maps
{
    public interface ITileLoader
    {
        GameObject CreateTile(
            int x,
            int y,
            int z,
            string relativeResourcePath,
            string spriteResourceName);
    }
}