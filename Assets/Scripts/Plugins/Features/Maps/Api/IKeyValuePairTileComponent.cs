using ProjectXyz.Game.Interface.Mapping;

namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface IKeyValuePairTileComponent : ITileComponent
    {
        string Key { get; }

        object Value { get; }
    }
}