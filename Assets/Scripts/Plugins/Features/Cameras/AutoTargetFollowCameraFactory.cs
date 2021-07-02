using Assets.Scripts.Plugins.Features.AnimatedWeather;
using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Cameras
{
    public sealed class AutoTargetFollowCameraFactory : IFollowCameraFactory
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly ICameraAutoTargetBehaviourStitcher _cameraAutoTargetStitcher;
        private readonly IWeatherSystemFactory _weatherSystemFactory;
        private readonly IMinimapCameraFactory _minimapCameraFactory;

        public AutoTargetFollowCameraFactory(
            IPrefabCreator prefabCreator,
            ICameraAutoTargetBehaviourStitcher cameraAutoTargetStitcher,
            IWeatherSystemFactory weatherSystemFactory,
            IMinimapCameraFactory minimapCameraFactory)
        {
            _prefabCreator = prefabCreator;
            _cameraAutoTargetStitcher = cameraAutoTargetStitcher;
            _weatherSystemFactory = weatherSystemFactory;
            _minimapCameraFactory = minimapCameraFactory;
        }

        public GameObject CreateCamera()
        {
            var followCamera = _prefabCreator.Create<GameObject>("Mapping/Prefabs/FollowCamera");
            followCamera.name = "FollowCamera";

            var weatherSystem = _weatherSystemFactory.Create();
            weatherSystem.transform.SetParent(followCamera.transform);

            _cameraAutoTargetStitcher.Attach(followCamera);

            var minimapCamera = _minimapCameraFactory.CreateCamera();
            minimapCamera.transform.SetParent(followCamera.transform);
            minimapCamera.transform.localPosition = new Vector3(
                minimapCamera.transform.localPosition.x,
                minimapCamera.transform.localPosition.y,
                0);

            return followCamera;
        }
    }
}