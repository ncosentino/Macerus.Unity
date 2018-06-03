using Assets.Scripts.Api.Scenes.Explore;
using Assets.Scripts.Unity;
using ProjectXyz.Api.Logging;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scenes.Explore.Input
{
    public sealed class GuiInputController : IGuiInputController
    {
        private readonly IDebugConsoleManager _debugConsoleManager;
        private readonly IKeyboardControls _keyboardControls;
        private readonly ILogger _logger;

        public GuiInputController(
            IDebugConsoleManager debugConsoleManager,
            IKeyboardControls keyboardControls,
            ILogger logger)
        {
            _debugConsoleManager = debugConsoleManager;
            _keyboardControls = keyboardControls;
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
            else if (UnityEngine.Input.GetKeyUp(_keyboardControls.DebugConsole))
            {
                _debugConsoleManager.Toggle();
            }
        }
    }
}
