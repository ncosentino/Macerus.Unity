using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Plugins.Features.Wip;
using Assets.Scripts.Unity.Input;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class PlayerQuickSlotControlsBehaviourStitcher : IPlayerQuickSlotControlsBehaviourStitcher
    {
        private readonly IDebugConsoleManager _debugConsoleManager;
        private readonly IKeyboardControls _keyboardControls;
        private readonly IKeyboardInput _keyboardInput;
        private readonly ILogger _logger;
        private readonly WipSkills _wipSkills;

        public PlayerQuickSlotControlsBehaviourStitcher(
            IKeyboardControls keyboardControls,
            IKeyboardInput keyboardInput,
            ILogger logger,
            WipSkills wipSkills,
            IDebugConsoleManager debugConsoleManager)
        {
            _keyboardControls = keyboardControls;
            _keyboardInput = keyboardInput;
            _logger = logger;
            _wipSkills = wipSkills;
            _debugConsoleManager = debugConsoleManager;
        }

        public void Attach(GameObject gameObject)
        {
            var playerQuickSlotControlsBehaviour = gameObject.AddComponent<PlayerQuickSlotControlsBehaviour>();
            playerQuickSlotControlsBehaviour.Logger = _logger;
            playerQuickSlotControlsBehaviour.KeyboardControls = _keyboardControls;
            playerQuickSlotControlsBehaviour.KeyboardInput = _keyboardInput;
            playerQuickSlotControlsBehaviour.DebugConsoleManager = _debugConsoleManager;
            playerQuickSlotControlsBehaviour.WipSkills = _wipSkills;
        }
    }
}
