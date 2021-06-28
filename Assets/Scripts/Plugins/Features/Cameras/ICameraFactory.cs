using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Cameras
{
    public interface ICameraFactory
    {
        GameObject CreateCamera();
    }
}