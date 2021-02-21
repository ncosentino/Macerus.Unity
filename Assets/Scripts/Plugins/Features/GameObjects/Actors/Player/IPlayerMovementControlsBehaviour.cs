using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;

using Macerus.Api.Behaviors;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IPlayerMovementControlsBehaviour : IReadOnlyPlayerMovementControlsBehaviour
    {
        new IKeyboardControls KeyboardControls { get; set; }

        new IMovementBehavior MovementBehavior { get; set; }

        new ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        new IDebugConsoleManager DebugConsoleManager { get; set; }
    }
}