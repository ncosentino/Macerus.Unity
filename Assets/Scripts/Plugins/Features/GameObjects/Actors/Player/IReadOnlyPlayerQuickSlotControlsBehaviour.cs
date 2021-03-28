
using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.Input;

using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.Logging;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IReadOnlyPlayerQuickSlotControlsBehaviour
    {
        IDebugConsoleManager DebugConsoleManager { get; }

        IKeyboardControls KeyboardControls { get; }

        IKeyboardInput KeyboardInput { get; }

        ILogger Logger { get; }

        ISkillUsage SkillUsage { get; }

        ISkillHandlerFacade SkillHandlerFacade { get; }
    }
}
