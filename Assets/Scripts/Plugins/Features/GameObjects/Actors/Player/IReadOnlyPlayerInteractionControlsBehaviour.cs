using Assets.Scripts.Input.Api;
using Assets.Scripts.Unity.Input;

using ProjectXyz.Api.Logging;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IReadOnlyPlayerInteractionControlsBehaviour
    {
        IKeyboardControls KeyboardControls { get; }

        IKeyboardInput KeyboardInput { get; }

        ILogger Logger { get; }

        IReadOnlyPlayerInteractionDetectionBehavior PlayerInteractionDetectionBehavior { get; }
    }
}