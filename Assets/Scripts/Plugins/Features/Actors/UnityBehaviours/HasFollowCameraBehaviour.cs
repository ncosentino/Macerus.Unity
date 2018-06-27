using Assets.Scripts.Scenes.Explore.Camera;
using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Framework.Contracts;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public sealed class HasFollowCameraBehaviour :
        MonoBehaviour,
        IHasFollowCameraBehaviour
    {
        public ICameraFactory CameraFactory { get; set; }

        public IObjectDestroyer ObjectDestroyer { get; set; }

        public GameObject ExploreGameObject { get; set; }

        private GameObject _followCamera;

        private void Start()
        {
            Contract.RequiresNotNull(
                CameraFactory,
                $"{nameof(CameraFactory)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                ExploreGameObject,
                $"{nameof(ExploreGameObject)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                ObjectDestroyer,
                $"{nameof(ObjectDestroyer)} was not set on '{gameObject}.{this}'.");

            _followCamera = CameraFactory.CreateCamera();
            _followCamera.transform.parent = ExploreGameObject.transform;
        }

        private void OnDestroy()
        {
            ObjectDestroyer.Destroy(_followCamera);
        }
    }
}