using Assets.Scripts.Unity.GameObjects;

namespace Assets.Scripts.Scenes.Explore.Camera
{
    public interface ICameraAutoTargetBehaviour : IReadOnlyCameraAutoTargetBehaviour
    {
        new IUnityGameObjectManager GameObjectManager { get; set; }

        new ICameraTargetting CameraTargetting { get; set; }
    }
}