namespace Assets.Scripts.Unity.Resources.Sprites
{
    public interface ISpriteSheetCache
    {
        void Add(string key, ISpriteSheet spriteSheet);

        bool TryGet(string key, out ISpriteSheet spriteSheet);
    }
}