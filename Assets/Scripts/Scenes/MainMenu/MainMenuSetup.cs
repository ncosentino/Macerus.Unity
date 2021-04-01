using Assets.Scripts.Console;
using Assets.Scripts.Gui;
using Assets.Scripts.Scenes.MainMenu.Gui.Views.MainMenu;
using Assets.Scripts.Scenes.MainMenu.Input;

using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;

using UnityEngine;

namespace Assets.Scripts.Scenes.MainMenu
{
    public sealed class MainMenuSetup : IMainMenuSetup
    {
        private readonly IGuiPrefabCreator _guiPrefabCreator;
        private readonly IViewWelderFactory _viewWelderFactory;
        private readonly IMainMenuView _mainMenuView;
        private readonly IGuiInputStitcher _guiInputStitcher;

        public MainMenuSetup(
            IGuiPrefabCreator guiPrefabCreator,
            IViewWelderFactory viewWelderFactory,
            IMainMenuView mainMenuView,
            IGuiInputStitcher guiInputStitcher)
        {
            _guiPrefabCreator = guiPrefabCreator;
            _viewWelderFactory = viewWelderFactory;
            _mainMenuView = mainMenuView;
            _guiInputStitcher = guiInputStitcher;
        }

        public void Setup()
        {
            var consoleObject = new GameObject()
            {
                name = "ConsoleCommands",
            };
            consoleObject.AddComponent<GlobalConsoleCommandsBehaviour>();

            var gui = _guiPrefabCreator.Create();
            _viewWelderFactory
                .Create<ISimpleWelder>(gui, _mainMenuView)
                .Weld();
            _guiInputStitcher.Attach(gui.GameObject);
        }
    }
}