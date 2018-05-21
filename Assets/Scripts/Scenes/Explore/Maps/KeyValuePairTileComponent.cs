namespace Assets.Scripts.Scenes.Explore.Maps
{
    public sealed class KeyValuePairTileComponent : IKeyValuePairTileComponent
    {
        public KeyValuePairTileComponent(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }

        public string Value { get; }
    }
}