namespace Assets.Scripts.Unity
{
    public interface IAssetPaths
    {
        string AssetsRoot { get; }

        string ResourcesRoot { get; }

        string MappingRoot { get; }

        string MapsRoot { get; }

        string TilesetsRoot { get; }
    }
}