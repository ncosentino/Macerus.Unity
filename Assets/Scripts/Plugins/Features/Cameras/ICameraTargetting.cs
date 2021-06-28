using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Cameras
{
    public interface ICameraTargetting
    {
        Transform CameraTarget { get; }

        void SetTarget(Transform target);
    }
}
