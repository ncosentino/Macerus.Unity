using Assets.Scripts.Scenes.Explore.Camera;
using Assets.Scripts.Unity.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IReadOnlyHasFollowCameraBehaviour
    {
        ICameraFactory CameraFactory { get; }

        GameObject ExploreGameObject { get; }

        IObjectDestroyer ObjectDestroyer { get; }
    }
}