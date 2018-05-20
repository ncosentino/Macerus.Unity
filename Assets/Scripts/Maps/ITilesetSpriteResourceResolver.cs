using Tiled.Net.Tilesets;

namespace Assets.Scripts.Maps
{
    public interface ITilesetSpriteResourceResolver
    {
        string ResolveResourcePath(ITileset tileset);
    }
}