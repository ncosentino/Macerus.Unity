using ProjectXyz.Game.Interface.Mapping;

namespace Assets.Scripts.Scenes.Explore.Maps
{
    public interface ITileResourceComponent : ITileComponent
    {
        string TilesetResourcePath { get; }

        string SpriteResourceName { get; }
    }
}