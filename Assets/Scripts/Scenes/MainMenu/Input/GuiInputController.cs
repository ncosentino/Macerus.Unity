using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;

namespace Assets.Scripts.Scenes.MainMenu.Input
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
                // TODO:
                // is a sub-menu open?
                // is the game running behind the main menu?
            }
        }
    }
}
