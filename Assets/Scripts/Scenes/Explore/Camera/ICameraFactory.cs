using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Camera
{
    public interface ICameraFactory
    {
        GameObject CreateCamera();
    }
}