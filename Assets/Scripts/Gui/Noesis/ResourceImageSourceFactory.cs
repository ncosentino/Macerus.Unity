using Assets.Scripts.Unity.Resources;

using Noesis;

using ProjectXyz.Api.Framework;

using UnityEngine;

namespace Assets.Scripts.Gui.Noesis
{
    public sealed class ResourceImageSourceFactory : IResourceImageSourceFactory
    {
        private readonly IResourceLoader _resourceLoader;

        public ResourceImageSourceFactory(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }

        public ImageSource CreateForResourceId(IIdentifier resourceId)
        {
            var relativePath = resourceId.ToString(); // FIXME: this is still a hack
            var texture = _resourceLoader.Load<Texture2D>(relativePath);
            var imageSource = new TextureSource(texture);
            return imageSource;
        }
    }
}