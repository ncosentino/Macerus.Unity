using ProjectXyz.Game.Interface.Mapping;

namespace Assets.Scripts.Scenes.Explore.Maps
{
    public interface IKeyValuePairTileComponent : ITileComponent
    {
        string Key { get; }

        string Value { get; }
    }
}