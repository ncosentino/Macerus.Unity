using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Camera
{
    public interface ICameraAutoTargetBehaviourStitcher
    {
        ICameraAutoTargetBehaviour Attach(GameObject cameraObject);
    }
}