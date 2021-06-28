using System;

using Assets.Scripts.Unity.GameObjects;

using Macerus.Plugins.Features.Camera;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Cameras
{
    public sealed class CameraAutoTargetBehaviourStitcher : ICameraAutoTargetBehaviourStitcher
    {
        private readonly IUnityGameObjectManager _gameObjectManager;
        private readonly IObservableCameraManager _cameraManager;

        public CameraAutoTargetBehaviourStitcher(
            IUnityGameObjectManager gameObjectManager,
            IObservableCameraManager cameraManager)
        {
            _gameObjectManager = gameObjectManager;
            _cameraManager = cameraManager;
        }

        public void Attach(GameObject cameraObject)
        {
            var cameraAutoTargetBehaviour = cameraObject.AddComponent<CameraAutoTargetBehaviour>();
            cameraAutoTargetBehaviour.CameraTargetting = cameraObject.GetRequiredComponent<ICameraTargetting>();
            cameraAutoTargetBehaviour.GameObjectManager = _gameObjectManager;
            cameraAutoTargetBehaviour.CameraManager = _cameraManager;
            cameraAutoTargetBehaviour.SearchDelay = TimeSpan.FromSeconds(0.25);
        }
    }
}