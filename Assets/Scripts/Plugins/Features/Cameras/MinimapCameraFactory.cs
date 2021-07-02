using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Cameras
{
    public sealed class MinimapCameraFactory : IMinimapCameraFactory
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly IResourceLoader _resourceLoader;

        public MinimapCameraFactory(
            IPrefabCreator prefabCreator,
            IResourceLoader resourceLoader)
        {
            _prefabCreator = prefabCreator;
            _resourceLoader = resourceLoader;
        }

        public GameObject CreateCamera()
        {
            var prefab = _prefabCreator.Create<GameObject>("Minimap/MinimapCamera");
            prefab.name = "MinimapCamera";

            var camera = prefab.GetRequiredComponent<Camera>();
            camera.targetTexture = _resourceLoader.Load<RenderTexture>("Minimap/MinimapRenderTexture");

            return prefab;
        }
    }
}