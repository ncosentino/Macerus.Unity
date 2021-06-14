using System.Linq;

using Assets.Scripts.Gui;
using Assets.Scripts.Plugins.Features.GameEngine;
using Assets.Scripts.Scenes.LoadingScreen.Gui;
using Assets.Scripts.Unity.GameObjects;

using Macerus.Plugins.Features.MainMenu.Api;

namespace Assets.Scripts.Scenes.LoadingScreen
{
    public sealed class LoadingScreenSetup : ILoadingScreenSetup
    {
        private readonly IUnityGameObjectManager _unityGameObjectManager;
        private readonly IGuiBehaviourStitcher _guiBehaviourStitcher;
        private readonly ILoadingScreenView _loadingScreenView;
        private readonly IMainMenuController _mainMenuController;
        private readonly IGameEngineUpdateBehaviourStitcher _gameEngineUpdateBehaviourStitcher;

        public LoadingScreenSetup(
            IUnityGameObjectManager unityGameObjectManager,
            IGuiBehaviourStitcher guiBehaviourStitcher,
            ILoadingScreenView loadingScreenView,
            IMainMenuController mainMenuController,
            IGameEngineUpdateBehaviourStitcher gameEngineUpdateBehaviourStitcher)
        {
            _unityGameObjectManager = unityGameObjectManager;
            _guiBehaviourStitcher = guiBehaviourStitcher;
            _loadingScreenView = loadingScreenView;
            _mainMenuController = mainMenuController;
            _gameEngineUpdateBehaviourStitcher = gameEngineUpdateBehaviourStitcher;
        }

        public void Setup()
        {
            var camera = _unityGameObjectManager
                .FindAll(x => x.activeSelf && x.name == "Camera")
                .Single();
            _guiBehaviourStitcher.Stitch(
                camera,
                x => x == camera,
                _loadingScreenView,
                null);

            _gameEngineUpdateBehaviourStitcher.Attach(_unityGameObjectManager
                .FindAll(x => x.activeSelf && x.name == "Root")
                .Single());

            _mainMenuController.OpenMenu();
        }
    }
}