using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Scripts.Unity.Resources
{
    public interface IResourceLoader
    {
        IReadOnlyCollection<TResource> LoadAll<TResource>(string relativeResourcePath)
            where TResource : Object;

        TResource Load<TResource>(string relativeResourcePath)
            where TResource : Object;

        Task<TResource> LoadAsync<TResource>(string relativeResourcePath)
            where TResource : Object;

        Task<byte[]> LoadBytesAsync(string relativeResourcePath);

        Task<Stream> LoadStreamAsync(string relativeResourcePath);

        string LoadText(string relativeResourcePath);
    }
}