using Assets.Scripts.Plugins.Features.AnimatedWeather;
using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Cameras
{
    public sealed class AutoTargetFollowCameraFactory : ICameraFactory
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly ICameraAutoTargetBehaviourStitcher _cameraAutoTargetStitcher;
        private readonly IWeatherSystemFactory _weatherSystemFactory;

        public AutoTargetFollowCameraFactory(
            IPrefabCreator prefabCreator,
            ICameraAutoTargetBehaviourStitcher cameraAutoTargetStitcher,
            IWeatherSystemFactory weatherSystemFactory)
        {
            _prefabCreator = prefabCreator;
            _cameraAutoTargetStitcher = cameraAutoTargetStitcher;
            _weatherSystemFactory = weatherSystemFactory;
        }

        public GameObject CreateCamera()
        {
            var followCamera = _prefabCreator.Create<GameObject>("Mapping/Prefabs/FollowCamera");
            followCamera.name = "FollowCamera";

            var weatherSystem = _weatherSystemFactory.Create();
            weatherSystem.transform.SetParent(followCamera.transform);

            _cameraAutoTargetStitcher.Attach(followCamera);

            return followCamera;
        }
    }
}