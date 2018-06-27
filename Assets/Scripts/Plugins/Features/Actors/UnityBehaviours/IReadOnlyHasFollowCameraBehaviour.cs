﻿using Assets.Scripts.Scenes.Explore.Camera;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public interface IReadOnlyHasFollowCameraBehaviour
    {
        ICameraFactory CameraFactory { get; }

        GameObject ExploreGameObject { get; }

        IObjectDestroyer ObjectDestroyer { get; }
    }
}