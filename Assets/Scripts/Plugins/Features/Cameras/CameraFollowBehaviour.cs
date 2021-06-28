using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Cameras
{
    public sealed class CameraFollowBehaviour :
        MonoBehaviour,
        ICameraTargetting
    {
        public float Dampening = 10;

        public Transform CameraTarget { get; private set; }

        public void SetTarget(Transform target)
        {
            CameraTarget = target;
        }

        public void FixedUpdate()
        {
            if (CameraTarget == null)
            {
                return;
            }

            var currentPosition = gameObject.transform.position;
            var targetPosition = CameraTarget.position;
            var destinationPosition = new Vector3(
                targetPosition.x,
                targetPosition.y,
                currentPosition.z);

            transform.position = Vector3.Lerp(
                currentPosition,
                destinationPosition,
                Time.deltaTime * Dampening);
        }
    }
}
