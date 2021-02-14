using System.Linq;
using Assets.Scripts.Api.Scenes.Explore;
using Assets.Scripts.Gui;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using ILogger = ProjectXyz.Api.Logging.ILogger;

namespace Assets.Scripts.Scenes.Explore.Input
{
    public sealed class GuiInputController : IGuiInputController
    {
        private readonly IDebugConsoleManager _debugConsoleManager;
        private readonly IKeyboardControls _keyboardControls;
        private readonly ILogger _logger;
        private readonly IUnityGameObjectManager _gameObjectManager;

        public GuiInputController(
            IDebugConsoleManager debugConsoleManager,
            IKeyboardControls keyboardControls,
            IUnityGameObjectManager gameObjectManager,
            ILogger logger)
        {
            _debugConsoleManager = debugConsoleManager;
            _keyboardControls = keyboardControls;
            _gameObjectManager = gameObjectManager;
            _logger = logger;
        }

        public void Update(float deltaTime)
        {
            if (UnityEngine.Input.GetKeyUp(_keyboardControls.GuiClose))
            {
                _logger.Debug("Opening main menu...");
                SceneManager.LoadScene("MainMenu");
                _logger.Debug("Opened main menu.");
            }
            else if (UnityEngine.Input.GetKeyUp(_keyboardControls.ToggleInventory))
            {
                _logger.Debug("Toggling inventory...");
                var inventoryUi = _gameObjectManager
                    .FindAll(x => x.name == "Inventory")
                    .First();
                inventoryUi.ToggleEnabled();
                _logger.Debug("Toggled inventory.");
            }
            else if (UnityEngine.Input.GetKeyUp(_keyboardControls.DebugConsole))
            {
                _debugConsoleManager.Toggle();
            }
        }
    }
}
