using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.Input;

using Macerus.Plugins.Features.StatusBar.Api;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class PlayerQuickSlotControlsBehaviourStitcher : IPlayerQuickSlotControlsBehaviourStitcher
    {
        private readonly IDebugConsoleManager _debugConsoleManager;
        private readonly IStatusBarController _statusBarController;
        private readonly IActorActionCheck _actorActionCheck;
        private readonly IScreenPointToMapCellConverter _screenPointToMapCellConverter;
        private readonly IKeyboardControls _keyboardControls;
        private readonly IKeyboardInput _keyboardInput;
        private readonly IMouseInput _mouseInput;
        private readonly ILogger _logger;

        public PlayerQuickSlotControlsBehaviourStitcher(
            IKeyboardControls keyboardControls,
            IKeyboardInput keyboardInput,
            IMouseInput mouseInput,
            ILogger logger,
            IDebugConsoleManager debugConsoleManager,
            IStatusBarController statusBarController,
            IActorActionCheck actorActionCheck,
            IScreenPointToMapCellConverter screenPointToMapCellConverter)
        {
            _keyboardControls = keyboardControls;
            _keyboardInput = keyboardInput;
            _mouseInput = mouseInput;
            _logger = logger;
            _debugConsoleManager = debugConsoleManager;
            _statusBarController = statusBarController;
            _actorActionCheck = actorActionCheck;
            _screenPointToMapCellConverter = screenPointToMapCellConverter;
        }

        public void Attach(GameObject unityGameObject, IGameObject actor)
        {
            var playerQuickSlotControlsBehaviour = unityGameObject.AddComponent<PlayerQuickSlotControlsBehaviour>();
            playerQuickSlotControlsBehaviour.Logger = _logger;
            playerQuickSlotControlsBehaviour.KeyboardControls = _keyboardControls;
            playerQuickSlotControlsBehaviour.KeyboardInput = _keyboardInput;
            playerQuickSlotControlsBehaviour.MouseInput = _mouseInput;
            playerQuickSlotControlsBehaviour.DebugConsoleManager = _debugConsoleManager;
            playerQuickSlotControlsBehaviour.StatusBarController = _statusBarController;
            playerQuickSlotControlsBehaviour.ActorActionCheck = _actorActionCheck;
            playerQuickSlotControlsBehaviour.ScreenPointToMapCellConverter = _screenPointToMapCellConverter;
            playerQuickSlotControlsBehaviour.Actor = actor;
        }
    }
}
