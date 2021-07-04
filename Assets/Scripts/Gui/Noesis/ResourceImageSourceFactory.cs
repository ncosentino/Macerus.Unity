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
            var texture = _resourceLoader.Load<UnityEngine.Texture>(relativePath);
            var imageSource = new TextureSource(texture);
            return imageSource;
        }
#else
        public ImageSource CreateForResourceId(IIdentifier resourceId)
        {
            var assetsRoot = new DirectoryInfo(@"..\..\..\Assets");
            var allResourcesDirectories = Directory.GetDirectories(
                assetsRoot.FullName,
                "Resources",
                SearchOption.AllDirectories);

            FileInfo fileInfo = null;
            var relativePath = resourceId.ToString(); // FIXME: this is still a hack
            foreach (var resourceDirectory in allResourcesDirectories)
            {
                var fullPathWithoutExtension = Path.Combine(
                    resourceDirectory,
                    relativePath);
                fileInfo = new FileInfo(fullPathWithoutExtension + ".png");
                if (fileInfo.Exists)
                {
                    break;
                }
                    
                fileInfo = new FileInfo(fullPathWithoutExtension + ".jpg");
                if (fileInfo.Exists)
                {
                    break;
                }

                fileInfo = new FileInfo(fullPathWithoutExtension + ".jpeg");
                if (fileInfo.Exists)
                {
                    break;
                }

                fileInfo = new FileInfo(fullPathWithoutExtension + ".renderTexture");
                if (fileInfo.Exists)
                {
                    // NOTE: these are unity-specific and won't work in Blend/VS
                    return null;
                }

                fileInfo = null;
            }

            if (fileInfo == null)
            {
                throw new FileNotFoundException(
                    $"Cannot find resource '{resourceId}'. Trying to " +
                    $"look for it within '{assetsRoot.FullName}' but " +
                    $"the attempted file extensions cannot find a match. " +
                    $"Please check the code in " +
                    $"'{GetType()}.{nameof(CreateForResourceId)}' to " +
                    $"see if there's additional support that should be " +
                    $"added.");
            }

            var uri = new Uri(fileInfo.FullName);
            var imageSource = new BitmapImage(uri);
            return imageSource;
        }
#endif
    }
}