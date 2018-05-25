using System.IO;
using Assets.Scripts.Unity;

namespace Assets.Scripts.Scenes.Explore.Maps.TiledNet
{
    public sealed class MapResourceIdConverter : IMapResourceIdConverter
    {
        private readonly string _relativeMapsResourceRoot;

        public MapResourceIdConverter(IAssetPaths assetPaths)
        {
            _relativeMapsResourceRoot = assetPaths
                .MapsRoot
                .Substring(assetPaths.ResourcesRoot.Length)
                .TrimStart('\\', '/');
        }

        public string Convert(string mapResourceId)
        {
            return Path.Combine(_relativeMapsResourceRoot, $"{mapResourceId}_tmx");
        }
    }
}