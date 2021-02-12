namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface ITilesetSpriteResourceResolver
    {
        string ResolveResourcePath(string tilesetResourcePath);
    }
}