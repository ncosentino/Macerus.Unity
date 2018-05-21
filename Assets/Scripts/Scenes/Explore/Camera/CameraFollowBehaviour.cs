using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Camera
{
    public class CameraFollowBehaviour : MonoBehaviour, ICameraTargetting
    {
        #region Unity Properties
        public float Dampening = 10;
        #endregion

        #region Properties
        public Transform CameraTarget { get; private set; }
        #endregion

        #region Methods
        public void SetTarget(Transform target)
        {
            CameraTarget = target;
        }

        public void Update()
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
        #endregion
    }
}
