﻿using System.Linq;

using Assets.Scripts.Gui;
using Assets.Scripts.Plugins.Features.Console;
using Assets.Scripts.Plugins.Features.GameEngine;
using Assets.Scripts.Scenes.MainMenu.Gui.MainMenu;
using Assets.Scripts.Scenes.MainMenu.Input;
using Assets.Scripts.Unity.GameObjects;

using Macerus.Plugins.Features.MainMenu.Api;

using UnityEngine;

namespace Assets.Scripts.Scenes.MainMenu
{
    public sealed class MainMenuSetup : IMainMenuSetup
    {
        private readonly IUnityGameObjectManager _unityGameObjectManager;
        private readonly IGuiBehaviourStitcher _guiBehaviourStitcher;
        private readonly IMainMenuView _mainMenuView;
        private readonly IMainMenuController _mainMenuController;
        private readonly IGuiInputStitcher _guiInputStitcher;
        private readonly IGameEngineUpdateBehaviourStitcher _gameEngineUpdateBehaviourStitcher;

        public MainMenuSetup(
            IUnityGameObjectManager unityGameObjectManager,
            IGuiBehaviourStitcher guiBehaviourStitcher,
            IMainMenuView mainMenuView,
            IMainMenuController mainMenuController,
            IGuiInputStitcher guiInputStitcher,
            IGameEngineUpdateBehaviourStitcher gameEngineUpdateBehaviourStitcher)
        {
            _unityGameObjectManager = unityGameObjectManager;
            _guiBehaviourStitcher = guiBehaviourStitcher;
            _mainMenuView = mainMenuView;
            _mainMenuController = mainMenuController;
            _guiInputStitcher = guiInputStitcher;
            _gameEngineUpdateBehaviourStitcher = gameEngineUpdateBehaviourStitcher;
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
                _mainMenuView,
                null);
            _guiInputStitcher.Attach(camera);

            _gameEngineUpdateBehaviourStitcher.Attach(_unityGameObjectManager
                .FindAll(x => x.activeSelf && x.name == "MainMenu")
                .Single());

            _mainMenuController.OpenMenu();
        }
    }
}