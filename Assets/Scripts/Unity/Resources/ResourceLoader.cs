using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Unity.Resources
{
    using Resources = UnityEngine.Resources;

    public sealed class ResourceLoader : IResourceLoader
    {
        public string LoadText(string relativeResourcePath)
        {
            var asset = Load<TextAsset>(relativeResourcePath);
            return asset.text;
        }

        public byte[] LoadBytes(string relativeResourcePath)
        {
            var asset = Load<TextAsset>(relativeResourcePath);
            return asset.bytes;
        }

        public Stream LoadStream(string relativeResourcePath)
        {
            var bytes = LoadBytes(relativeResourcePath);
            return new MemoryStream(bytes);
        }

        public TResource Load<TResource>(string relativeResourcePath)
            where TResource : UnityEngine.Object
        {
            var uncasted = Resources.Load(relativeResourcePath);
            if (uncasted == null)
            {
                throw new InvalidOperationException(
                    $"No resource with relative resource path " +
                    $"'{relativeResourcePath}' could be loaded.");
            }

            var casted = uncasted as TResource;
            if (casted == null)
            {
                throw new InvalidOperationException(
                    $"Resource with relative resource path " +
                    $"'{relativeResourcePath}' was of type '{casted.GetType()}' " +
                    $"and not of type '{typeof(TResource)}'.");
            }

            return casted;
        }
    }
}
