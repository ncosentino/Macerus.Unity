using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public IReadOnlyCollection<TResource> LoadAll<TResource>(string relativeResourcePath)
            where TResource : UnityEngine.Object
        {
            var resources = Resources.LoadAll<TResource>(relativeResourcePath);
            if (resources == null || !resources.Any())
            {
                throw new InvalidOperationException(
                    $"No resources with relative resource path " +
                    $"'{relativeResourcePath}' could be loaded.");
            }

            return resources;
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
                    $"'{relativeResourcePath}' was of type '{uncasted.GetType()}' " +
                    $"and not of type '{typeof(TResource)}'.");
            }

            return casted;
        }
    }
}
