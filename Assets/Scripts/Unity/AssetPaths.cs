using System.IO;
using UnityEngine;

namespace Assets.Scripts.Unity
{
    public sealed class AssetPaths : IAssetPaths
    {
        public AssetPaths()
        {
            AssetsRoot = Application.dataPath;
            ResourcesRoot = Path.Combine(AssetsRoot, "Resources");
            MappingRoot = Path.Combine(ResourcesRoot, "Mapping");
            MapsRoot = Path.Combine(MappingRoot, "Maps");
            TilesetsRoot = Path.Combine(MappingRoot, "Tilesets");
        }

        public string AssetsRoot { get; }

        public string ResourcesRoot { get; }

        public string MappingRoot { get; }

        public string MapsRoot { get; }

        public string TilesetsRoot { get; }
    }
}