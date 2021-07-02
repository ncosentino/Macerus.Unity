using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Cameras
{
    public interface IFollowCameraFactory
    {
        GameObject CreateCamera();
    }
}