using System.Linq;

using Assets.Scripts.Gui;
using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.GameObjects;

using Macerus.Game.Api;
using Macerus.Plugins.Features.CharacterSheet.Api;
using Macerus.Plugins.Features.Inventory.Api;

using ProjectXyz.Shared.Framework;

using UnityEngine.SceneManagement;

using ILogger = ProjectXyz.Api.Logging.ILogger;

namespace Assets.Scripts.Scenes.Explore.Input
{
    public sealed class GuiInputController : IGuiInputController
    {
        private readonly IDebugConsoleManager _debugConsoleManager;
        private readonly IKeyboardControls _keyboardControls;
        private readonly ILogger _logger;
        private readonly IPlayerInventoryController _playerInventoryController;
        private readonly ICharacterSheetController _characterSheetController;
        private readonly IUnityGameObjectManager _gameObjectManager;
        private readonly ISceneManager _sceneManager;

        public GuiInputController(
            IDebugConsoleManager debugConsoleManager,
            IKeyboardControls keyboardControls,
            IUnityGameObjectManager gameObjectManager,
            ISceneManager sceneManager,
            ILogger logger,
            IPlayerInventoryController playerInventoryController,
            ICharacterSheetController characterSheetController)
        {
            _debugConsoleManager = debugConsoleManager;
            _keyboardControls = keyboardControls;
            _gameObjectManager = gameObjectManager;
            _sceneManager = sceneManager;
            _logger = logger;
            _playerInventoryController = playerInventoryController;
            _characterSheetController = characterSheetController;
        }

        public void Update(float deltaTime)
        {
            if (_debugConsoleManager.GetConsoleWindowVisible())
            {
                return;
            }

            if (UnityEngine.Input.GetKeyUp(_keyboardControls.GuiClose))
            {
                _sceneManager.GoToScene(new StringIdentifier("MainMenu"));
            }
            else if (UnityEngine.Input.GetKeyUp(_keyboardControls.ToggleInventory))
            {
                _playerInventoryController.ToggleInventory();
            }
            else if (UnityEngine.Input.GetKeyUp(_keyboardControls.ToggleCharacterSheet))
            {
                _characterSheetController.ToggleCharacterSheet();
            }
        }
    }
}
