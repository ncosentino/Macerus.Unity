using System.IO;
using Assets.Scripts.Scenes.Explore.Maps;
using Assets.Scripts.Unity;
using UnityEngine;

namespace Assets.Scripts.TiledNet
{
    public sealed class MapResourceIdConverter : IMapResourceIdConverter
    {
        private readonly string _relativeMapsResourceRoot;

        public MapResourceIdConverter(IAssetPaths assetPaths)
        {
            _relativeMapsResourceRoot = assetPaths
                .MapsRoot
                .Substring(assetPaths.ResourcesRoot.Length)
                .TrimStart('\\', '/');
        }

        public string Convert(string mapResourceId)
        {
            Debug.Log($"Map resource id {mapResourceId}");
            return Path.Combine(_relativeMapsResourceRoot, $"{mapResourceId}_tmx");
        }
    }
}