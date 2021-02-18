﻿using Assets.Scripts.Scenes.Explore.Camera;
using Assets.Scripts.Unity.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IHasFollowCameraBehaviour : IReadOnlyHasFollowCameraBehaviour
    {
        new ICameraFactory CameraFactory { get; set; }

        new GameObject ExploreGameObject { get; set; }

        new IObjectDestroyer ObjectDestroyer { get; set; }
    }
}