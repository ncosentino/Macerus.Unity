using ProjectXyz.Game.Interface.Mapping;

namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface ITileResourceComponent : ITileComponent
    {
        string TilesetResourcePath { get; }

        string SpriteResourceName { get; }
    }
}