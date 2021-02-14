using System;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Camera
{
    public sealed class CameraAutoTargetBehaviourStitcher : ICameraAutoTargetBehaviourStitcher
    {
        private readonly IUnityGameObjectManager _gameObjectManager;

        public CameraAutoTargetBehaviourStitcher(IUnityGameObjectManager gameObjectManager)
        {
            _gameObjectManager = gameObjectManager;
        }

        public ICameraAutoTargetBehaviour Attach(GameObject cameraObject)
        {
            var cameraAutoTargetBehaviour = cameraObject.AddComponent<CameraAutoTargetBehaviour>();
            cameraAutoTargetBehaviour.CameraTargetting = cameraObject.GetRequiredComponent<ICameraTargetting>();
            cameraAutoTargetBehaviour.GameObjectManager = _gameObjectManager;
            cameraAutoTargetBehaviour.SearchDelay = TimeSpan.FromSeconds(0.25);

            return cameraAutoTargetBehaviour;
        }
    }
}