using Assets.Scripts.Unity;

using Macerus.Plugins.Features.Mapping;

namespace Assets.Scripts.Plugins.Features.Maps.TiledNet.Autofac
{
    public sealed class MappingAssetPaths : IMappingAssetPaths
    {
        private readonly IAssetPaths _assetPaths;

        public MappingAssetPaths(IAssetPaths assetPaths)
        {
            _assetPaths = assetPaths;
        }

        public string MapsRoot => _assetPaths.MapsRoot;

        public string ResourcesRoot => _assetPaths.ResourcesRoot;
    }
}
