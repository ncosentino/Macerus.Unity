
using Assets.Scripts.Unity;

using Macerus.Api.Behaviors;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors
{
    public interface IDrawWalkPathBehaviour
    {
        LineRenderer LineRenderer { get; set; }

        IReadOnlyMovementBehavior MovementBehavior { get; set; }

        ITimeProvider TimeProvider { get; set; }

        IReadOnlyWorldLocationBehavior WorldLocationBehavior { get; set; }
    }
}
