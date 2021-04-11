using Assets.Scripts;
using Assets.Scripts.Gui;
using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.Controls;
using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class PlayerMovementControlsBehaviourStitcher : IPlayerMovementControlsBehaviourStitcher
    {
        private readonly IKeyboardControls _keyboardControls;
        private readonly IKeyboardInput _keyboardInput;
        private readonly IMouseInput _mouseInput;
        private readonly IGuiHitTester _guiHitTester;
        private readonly ILogger _logger;
        private readonly IDebugConsoleManager _debugConsoleManager;
        private readonly IScreenPointToMapCellConverter _screenPointToMapCellConverter;
        private readonly IPlayerControlConfiguration _playerControlConfiguration;

        public PlayerMovementControlsBehaviourStitcher(
            IKeyboardControls keyboardControls,
            IKeyboardInput keyboardInput,
            IMouseInput mouseInput,
            IGuiHitTester guiHitTester,
            ILogger logger,
            IDebugConsoleManager debugConsoleManager,
            IScreenPointToMapCellConverter screenPointToMapCellConverter,
            IPlayerControlConfiguration playerControlConfiguration)
        {
            _keyboardControls = keyboardControls;
            _keyboardInput = keyboardInput;
            _mouseInput = mouseInput;
            _guiHitTester = guiHitTester;
            _logger = logger;
            _debugConsoleManager = debugConsoleManager;
            _screenPointToMapCellConverter = screenPointToMapCellConverter;
            _playerControlConfiguration = playerControlConfiguration;
        }

        public void Attach(GameObject gameObject)
        {
            var playerInputControlsBehaviour = gameObject.AddComponent<PlayerMovementControlsBehaviour>();
            playerInputControlsBehaviour.Logger = _logger;
            playerInputControlsBehaviour.KeyboardControls = _keyboardControls;
            playerInputControlsBehaviour.KeyboardInput = _keyboardInput;
            playerInputControlsBehaviour.MouseInput = _mouseInput;
            playerInputControlsBehaviour.DebugConsoleManager = _debugConsoleManager;
            playerInputControlsBehaviour.GuiHitTester = _guiHitTester;
            playerInputControlsBehaviour.ScreenPointToMapCellConverter = _screenPointToMapCellConverter;
            playerInputControlsBehaviour.PlayerControlConfiguration = _playerControlConfiguration;
            playerInputControlsBehaviour.MovementBehavior = gameObject
                .GetRequiredComponent<IReadOnlyHasGameObject>()
                .GameObject
                .GetOnly<IMovementBehavior>();            
        }
    }
}