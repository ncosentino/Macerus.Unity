using Assets.Scripts.Unity.GameObjects;

namespace Assets.Scripts.Scenes.Explore.Camera
{
    public interface IReadOnlyCameraAutoTargetBehaviour
    {
        IGameObjectManager GameObjectManager { get;  }

        ICameraTargetting CameraTargetting { get;  }
    }
}