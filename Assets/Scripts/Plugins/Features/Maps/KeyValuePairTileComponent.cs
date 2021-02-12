using Assets.Scripts.Plugins.Features.Maps.Api;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public sealed class KeyValuePairTileComponent : IKeyValuePairTileComponent
    {
        public KeyValuePairTileComponent(string key, object value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }

        public object Value { get; }
    }
}