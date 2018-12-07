using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Unity.Resources
{
    public interface IResourceLoader
    {
        IReadOnlyCollection<TResource> LoadAll<TResource>(string relativeResourcePath)
            where TResource : Object;

        TResource Load<TResource>(string relativeResourcePath) where TResource : Object;

        byte[] LoadBytes(string relativeResourcePath);

        Stream LoadStream(string relativeResourcePath);

        string LoadText(string relativeResourcePath);
    }
}