using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Camera
{
    public interface ICameraTargetting
    {
        #region Properties
        Transform CameraTarget { get; }
        #endregion

        #region Methods
        void SetTarget(Transform target);
        #endregion
    }
}
