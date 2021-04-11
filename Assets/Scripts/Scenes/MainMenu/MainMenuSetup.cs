using System.Linq;

using Assets.Scripts.Gui;
using Assets.Scripts.Plugins.Features.Console;
using Assets.Scripts.Scenes.MainMenu.Gui.Views.MainMenu;
using Assets.Scripts.Scenes.MainMenu.Input;
using Assets.Scripts.Unity.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Scenes.MainMenu
{
    public sealed class MainMenuSetup : IMainMenuSetup
    {
        private readonly IUnityGameObjectManager _unityGameObjectManager;
        private readonly IGuiBehaviourStitcher _guiBehaviourStitcher;
        private readonly IMainMenuView _mainMenuView;
        private readonly IGuiInputStitcher _guiInputStitcher;

        public MainMenuSetup(
            IUnityGameObjectManager unityGameObjectManager,
            IGuiBehaviourStitcher guiBehaviourStitcher,
            IMainMenuView mainMenuView,
            IGuiInputStitcher guiInputStitcher)
        {
            _unityGameObjectManager = unityGameObjectManager;
            _guiBehaviourStitcher = guiBehaviourStitcher;
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

            var camera = _unityGameObjectManager
                .FindAll(x => x.activeSelf && x.name == "Camera")
                .Single();

            _guiBehaviourStitcher.Stitch(
                camera,
                x => x == camera,
                _mainMenuView);
            _guiInputStitcher.Attach(camera);
        }
    }
}