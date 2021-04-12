using Assets.Scripts.Unity.GameObjects;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Camera
{
    public sealed class HasFollowCameraBehaviour : MonoBehaviour
    {
        public ICameraFactory CameraFactory { get; set; }

        public IObjectDestroyer ObjectDestroyer { get; set; }

        public GameObject ExploreGameObject { get; set; }

        private GameObject _followCamera;

        private void Start()
        {
            this.RequiresNotNull(CameraFactory, nameof(CameraFactory));
            this.RequiresNotNull(ObjectDestroyer, nameof(ObjectDestroyer));
            this.RequiresNotNull(ExploreGameObject, nameof(ExploreGameObject));

            _followCamera = CameraFactory.CreateCamera();
            _followCamera.transform.parent = ExploreGameObject.transform;
        }

        private void OnDestroy()
        {
            ObjectDestroyer.Destroy(_followCamera);
        }
    }
}