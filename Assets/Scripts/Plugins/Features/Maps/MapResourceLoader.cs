using System.IO;
using System.Threading.Tasks;

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

        public async Task<Stream> LoadStreamAsync(string pathToResource)
        {
            return await _resourceLoader.LoadStreamAsync(pathToResource);
        }
    }
}
