using Assets.Scripts.Unity;

using Macerus.Api.Behaviors;

using ProjectXyz.Plugins.Features.Mapping;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors
{
    public interface IDrawWalkPathBehaviour
    {
        LineRenderer LineRenderer { get; set; }

        IReadOnlyMovementBehavior MovementBehavior { get; set; }

        ITimeProvider TimeProvider { get; set; }

        IReadOnlyPositionBehavior ReadOnlyPositionBehavior { get; set; }
    }
}
