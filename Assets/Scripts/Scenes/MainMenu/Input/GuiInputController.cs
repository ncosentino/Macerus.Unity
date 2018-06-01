﻿using Assets.Scripts.Unity;

namespace Assets.Scripts.Scenes.MainMenu.Input
{
    public sealed class GuiInputController : IGuiInputController
    {
        private readonly IDebugConsoleManager _debugConsoleManager;
        private readonly Api.Scenes.Explore.IKeyboardControls _keyboardControls;

        public GuiInputController(
            IDebugConsoleManager debugConsoleManager,
            Api.Scenes.Explore.IKeyboardControls keyboardControls)
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
            else if (UnityEngine.Input.GetKeyUp(_keyboardControls.DebugConsole))
            {
                _debugConsoleManager.Toggle();
            }
        }
    }
}
