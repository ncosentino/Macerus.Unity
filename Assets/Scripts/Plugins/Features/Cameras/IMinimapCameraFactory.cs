using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Cameras
{
    public interface IMinimapCameraFactory
    {
        GameObject CreateCamera();
    }
}