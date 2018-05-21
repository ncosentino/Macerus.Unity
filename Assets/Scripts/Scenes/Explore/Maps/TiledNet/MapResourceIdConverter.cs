using System.IO;
using Assets.Scripts.Unity;

namespace Assets.Scripts.Scenes.Explore.Maps.TiledNet
{
    public sealed class MapResourceIdConverter : IMapResourceIdConverter
    {
        private readonly IAssetPaths _assetPaths;

        public MapResourceIdConverter(IAssetPaths assetPaths)
        {
            _assetPaths = assetPaths;
        }

        public string Convert(string mapResourceId)
        {
            return Path.Combine(_assetPaths.MapsRoot, $"{mapResourceId}.tmx");
        }
    }
}