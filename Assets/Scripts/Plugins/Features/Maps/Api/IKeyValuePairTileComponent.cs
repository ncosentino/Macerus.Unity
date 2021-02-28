using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface IKeyValuePairTileComponent : ITileComponent
    {
        string Key { get; }

        object Value { get; }
    }
}