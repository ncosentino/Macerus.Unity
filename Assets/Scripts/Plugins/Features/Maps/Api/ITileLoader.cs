using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps.Api
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