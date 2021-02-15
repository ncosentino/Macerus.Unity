using Assets.Scripts.Input.Api;

using Macerus.Api.Behaviors;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IReadOnlyPlayerMovementControlsBehaviour
    {
        IKeyboardControls KeyboardControls { get; }

        IMovementBehavior MovementBehavior { get; }

        ProjectXyz.Api.Logging.ILogger Logger { get; }
    }
}