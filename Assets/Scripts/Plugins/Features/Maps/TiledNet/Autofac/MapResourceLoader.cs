using System.IO;

using Assets.Scripts.Unity.Resources;

using Macerus.Plugins.Features.Mapping;
using Macerus.Plugins.Features.Mapping.TiledNet;

namespace Assets.Scripts.Plugins.Features.Maps.TiledNet.Autofac
{
    public sealed class MapResourceLoader : ITiledMapResourceLoader
    {
        private readonly IResourceLoader _resourceLoader;

        public MapResourceLoader(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }

        public Stream LoadStream(string pathToResource)
        {
            return _resourceLoader.LoadStream(pathToResource);
        }
    }
}
