using Assets.Scripts.Api.Scenes.Explore;
using Macerus.Api.Behaviors;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public interface IReadOnlyPlayerInputControlsBehaviour
    {
        IKeyboardControls KeyboardControls { get; }

        IMovementBehavior MovementBehavior { get; }

        ProjectXyz.Api.Logging.ILogger Logger { get; }
    }
}