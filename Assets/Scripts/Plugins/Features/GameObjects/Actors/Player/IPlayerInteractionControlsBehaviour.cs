using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.Input;

using ProjectXyz.Api.Logging;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IPlayerInteractionControlsBehaviour : IReadOnlyPlayerInteractionControlsBehaviour
    {
        new IDebugConsoleManager DebugConsoleManager { get; set; }

        new IKeyboardControls KeyboardControls { get; set; }

        new IKeyboardInput KeyboardInput { get; set; }

        new ILogger Logger { get; set; }

        new IReadOnlyPlayerInteractionDetectionBehavior PlayerInteractionDetectionBehavior { get; set; }
    }
}