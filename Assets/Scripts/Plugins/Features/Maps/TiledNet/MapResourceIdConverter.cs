using System.IO;

using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity;

namespace Assets.Scripts.Plugins.Features.Maps.TiledNet
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class MapResourceIdConverter : IMapResourceIdConverter
    {
        private readonly string _relativeMapsResourceRoot;
        private readonly ILogger _logger;

        public MapResourceIdConverter(
            IAssetPaths assetPaths,
            ILogger logger)
        {
            _logger = logger;
            _relativeMapsResourceRoot = assetPaths
                .MapsRoot
                .Substring(assetPaths.ResourcesRoot.Length)
                .TrimStart('\\', '/');
        }

        public string Convert(string mapResourceId)
        {
            _logger.Debug($"Map resource id {mapResourceId}");
            return Path.Combine(_relativeMapsResourceRoot, $"{mapResourceId}_tmx");
        }
    }
}