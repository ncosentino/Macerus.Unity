using System.IO;

using Assets.Scripts.Unity.Resources;

using Macerus.Plugins.Features.Mapping;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public sealed class MapResourceLoader : IMapResourceLoader
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
