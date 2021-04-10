using UnityEngine.Tilemaps;

namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface ITileLoader
    {
        Tile LoadTile(
            string relativeResourcePath,
            string spriteResourceName);
    }
}