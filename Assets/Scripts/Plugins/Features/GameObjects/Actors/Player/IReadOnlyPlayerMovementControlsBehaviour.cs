using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.Controls;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;

using Macerus.Api.Behaviors;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IReadOnlyPlayerMovementControlsBehaviour
    {
        IKeyboardControls KeyboardControls { get; }

        IMovementBehavior MovementBehavior { get; }

        ProjectXyz.Api.Logging.ILogger Logger { get; }

        IDebugConsoleManager DebugConsoleManager { get; }

        IPlayerControlConfiguration PlayerControlConfiguration { get; }
    }
}