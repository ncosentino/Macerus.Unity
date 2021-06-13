using Assets.Scripts.Unity.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Camera
{
    public sealed class HasFollowCameraBehaviourStitcher : IHasFollowCameraBehaviourStitcher
    {
        private readonly ICameraFactory _cameraFactory;
        private readonly IObjectDestroyer _objectDestroyer;

        public HasFollowCameraBehaviourStitcher(
            ICameraFactory cameraFactory,
            IObjectDestroyer objectDestroyer)
        {
            _cameraFactory = cameraFactory;
            _objectDestroyer = objectDestroyer;
        }

        public void Attach(GameObject gameObject)
        {
            var hasFollowCameraBehaviour = gameObject.AddComponent<HasFollowCameraBehaviour>();
            hasFollowCameraBehaviour.CameraFactory = _cameraFactory;
            hasFollowCameraBehaviour.ObjectDestroyer = _objectDestroyer;
            hasFollowCameraBehaviour.ExploreGameObject = gameObject;
        }
    }
}