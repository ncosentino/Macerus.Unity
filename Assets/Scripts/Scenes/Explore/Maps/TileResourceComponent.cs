namespace Assets.Scripts.Scenes.Explore.Maps
{
    public sealed class TileResourceComponent : ITileResourceComponent
    {
        public TileResourceComponent(
            string tilesetResourcePath,
            string spriteResourceName)
        {
            TilesetResourcePath = tilesetResourcePath;
            SpriteResourceName = spriteResourceName;
        }

        public string TilesetResourcePath { get; }

        public string SpriteResourceName { get; }
    }
}