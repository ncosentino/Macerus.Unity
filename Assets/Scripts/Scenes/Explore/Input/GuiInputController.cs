using Assets.Scripts.Api.Scenes.Explore;
using Assets.Scripts.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scenes.Explore.Input
{
    public sealed class GuiInputController : IGuiInputController
    {
        private readonly IDebugConsoleManager _debugConsoleManager;
        private readonly IKeyboardControls _keyboardControls;

        public GuiInputController(
            IDebugConsoleManager debugConsoleManager,
            IKeyboardControls keyboardControls)
        {
            _debugConsoleManager = debugConsoleManager;
            _keyboardControls = keyboardControls;
        }

        public void Update(float deltaTime)
        {
            if (UnityEngine.Input.GetKeyUp(_keyboardControls.GuiClose))
            {
                Debug.Log("Opening main menu...");
                SceneManager.LoadScene("MainMenu");
                Debug.Log("Opened main menu.");
            }
            else if (UnityEngine.Input.GetKeyUp(_keyboardControls.DebugConsole))
            {
                _debugConsoleManager.Toggle();
            }
        }
    }
}
