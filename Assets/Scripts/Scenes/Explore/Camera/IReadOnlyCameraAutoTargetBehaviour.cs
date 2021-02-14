using Assets.Scripts.Unity.GameObjects;

namespace Assets.Scripts.Scenes.Explore.Camera
{
    public interface IReadOnlyCameraAutoTargetBehaviour
    {
        IUnityGameObjectManager GameObjectManager { get;  }

        ICameraTargetting CameraTargetting { get;  }
    }
}