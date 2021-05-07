using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.Input;

using Macerus.Plugins.Features.StatusBar.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class PlayerQuickSlotControlsBehaviourStitcher : IPlayerQuickSlotControlsBehaviourStitcher
    {
        private readonly IDebugConsoleManager _debugConsoleManager;
        private readonly IStatusBarController _statusBarController;
        private readonly IKeyboardControls _keyboardControls;
        private readonly IKeyboardInput _keyboardInput;
        private readonly ILogger _logger;

        public PlayerQuickSlotControlsBehaviourStitcher(
            IKeyboardControls keyboardControls,
            IKeyboardInput keyboardInput,
            ILogger logger,
            IDebugConsoleManager debugConsoleManager,
            IStatusBarController statusBarController)
        {
            _keyboardControls = keyboardControls;
            _keyboardInput = keyboardInput;
            _logger = logger;
            _debugConsoleManager = debugConsoleManager;
            _statusBarController = statusBarController;
        }

        public void Attach(GameObject gameObject)
        {
            var playerQuickSlotControlsBehaviour = gameObject.AddComponent<PlayerQuickSlotControlsBehaviour>();
            playerQuickSlotControlsBehaviour.Logger = _logger;
            playerQuickSlotControlsBehaviour.KeyboardControls = _keyboardControls;
            playerQuickSlotControlsBehaviour.KeyboardInput = _keyboardInput;
            playerQuickSlotControlsBehaviour.DebugConsoleManager = _debugConsoleManager;
            playerQuickSlotControlsBehaviour.StatusBarController = _statusBarController;
        }
    }
}
