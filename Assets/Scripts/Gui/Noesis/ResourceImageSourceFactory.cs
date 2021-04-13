#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Unity.Resources;

using Noesis;

using UnityEngine;
#else
using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
#endif

using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Gui.Noesis
{
    public sealed class ResourceImageSourceFactory : IResourceImageSourceFactory
    {
#if NOESIS
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
#else
        public ImageSource CreateForResourceId(IIdentifier resourceId)
        {
            var relativePath = resourceId.ToString(); // FIXME: this is still a hack
            var fullPath = Path.Combine(@"..\..\..\Assets\Resources", relativePath + ".png");
            var fileInfo = new FileInfo(fullPath);
            var uri = new Uri(fileInfo.FullName);
            var imageSource = new BitmapImage(uri);
            return imageSource;
        }
#endif
    }
}