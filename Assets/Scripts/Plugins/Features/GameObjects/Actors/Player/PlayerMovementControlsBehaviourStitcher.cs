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
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class PlayerMovementControlsBehaviourStitcher : IPlayerMovementControlsBehaviourStitcher
    {
        private readonly IKeyboardControls _keyboardControls;
        private readonly IKeyboardInput _keyboardInput;
        private readonly IMouseInput _mouseInput;
        private readonly IGuiHitTesterFacade _guiHitTester;
        private readonly ILogger _logger;
        private readonly IDebugConsoleManager _debugConsoleManager;
        private readonly IScreenPointToMapCellConverter _screenPointToMapCellConverter;
        private readonly IPlayerControlConfiguration _playerControlConfiguration;
        private readonly IMapManager _mapManager;

        public PlayerMovementControlsBehaviourStitcher(
            IKeyboardControls keyboardControls,
            IKeyboardInput keyboardInput,
            IMouseInput mouseInput,
            IGuiHitTesterFacade guiHitTester,
            ILogger logger,
            IDebugConsoleManager debugConsoleManager,
            IScreenPointToMapCellConverter screenPointToMapCellConverter,
            IPlayerControlConfiguration playerControlConfiguration,
            IMapManager mapManager)
        {
            _keyboardControls = keyboardControls;
            _keyboardInput = keyboardInput;
            _mouseInput = mouseInput;
            _guiHitTester = guiHitTester;
            _logger = logger;
            _debugConsoleManager = debugConsoleManager;
            _screenPointToMapCellConverter = screenPointToMapCellConverter;
            _playerControlConfiguration = playerControlConfiguration;
            _mapManager = mapManager;
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
            playerInputControlsBehaviour.MapManager = _mapManager;
            playerInputControlsBehaviour.MovementBehavior = gameObject.GetOnly<IMovementBehavior>();
            playerInputControlsBehaviour.SizeBehavior = gameObject.GetOnly<IReadOnlySizeBehavior>();
            playerInputControlsBehaviour.PositionBehavior = gameObject.GetOnly<IReadOnlyPositionBehavior>();
        }
    }
}