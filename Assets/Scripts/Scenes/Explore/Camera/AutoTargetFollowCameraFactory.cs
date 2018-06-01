using Assets.Scripts.Unity.Resources;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Camera
{
    public sealed class AutoTargetFollowCameraFactory : ICameraFactory
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly ICameraAutoTargetBehaviourStitcher _cameraAutoTargetStitcher;

        public AutoTargetFollowCameraFactory(
            IPrefabCreator prefabCreator,
            ICameraAutoTargetBehaviourStitcher cameraAutoTargetStitcher)
        {
            _prefabCreator = prefabCreator;
            _cameraAutoTargetStitcher = cameraAutoTargetStitcher;
        }

        public GameObject CreateCamera()
        {
            var followCamera = _prefabCreator.Create<GameObject>("Mapping/Prefabs/FollowCamera");
            followCamera.name = "FollowCamera";

            _cameraAutoTargetStitcher.Attach(followCamera);

            return followCamera;
        }
    }
}