
using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.Input;

using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.Logging;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IPlayerQuickSlotControlsBehaviour : IReadOnlyPlayerQuickSlotControlsBehaviour
    {
        new IDebugConsoleManager DebugConsoleManager { get; set; }

        new IKeyboardControls KeyboardControls { get; set; }

        new IKeyboardInput KeyboardInput { get; set; }

        new ILogger Logger { get; set; }

        new ISkillUsage SkillUsage { get; set; }
        
        new ISkillHandlerFacade SkillHandlerFacade { get; set; }
    }
}
