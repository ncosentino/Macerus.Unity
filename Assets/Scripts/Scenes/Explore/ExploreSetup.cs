using System;
using System.Linq;

using Assets.Scripts.Gui;
using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Gui.Unity;
using Assets.Scripts.Plugins.Features.Cameras;
using Assets.Scripts.Plugins.Features.Console;
using Assets.Scripts.Plugins.Features.GameEngine;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Plugins.Features.NewHud.Noesis;
using Assets.Scripts.Scenes.Explore.Api;
using Assets.Scripts.Scenes.Explore.Console;
using Assets.Scripts.Scenes.Explore.Input;
using Assets.Scripts.Unity.GameObjects;

using Macerus.Plugins.Features.Hud;
using Macerus.Plugins.Features.InGameMenu.Api;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.Explore
{
    public sealed class ExploreSetup : IExploreSetup
    {
        private readonly IUnityGameObjectManager _gameObjectManager;
        private readonly IMapPrefabFactory _mapPrefabFactory;
        private readonly IGuiInputStitcher _guiInputStitcher;
        private readonly IExploreSceneStartupInterceptorFacade _exploreSceneStartupInterceptorFacade;
        private readonly IGameEngineUpdateBehaviourStitcher _gameEngineUpdateBehaviourStitcher;
        private readonly IGuiBehaviourStitcher _guiBehaviorStitcher;
        private readonly IUnityGuiHitTester _unityGuiHitTester;
        private readonly INoesisGuiHitTester _noesisGuiHitTester;
        private readonly IExploreGameRootPrefabFactory _exploreGameRootPrefabFactory;
        private readonly IHasFollowCameraBehaviourStitcher _hasFollowCameraBehaviourStitcher;
        private readonly Lazy<IHudController> _lazyHudController;
        private readonly Lazy<IInGameMenuController> _lazyInGameMenuController;
        private readonly Lazy<IHudView> _lazyHudView;

        public ExploreSetup(
            IUnityGameObjectManager gameObjectManager,
            IMapPrefabFactory mapPrefabFactory,
            IGuiInputStitcher guiInputStitcher,
            IExploreSceneStartupInterceptorFacade exploreSceneStartupInterceptorFacade,
            IGameEngineUpdateBehaviourStitcher gameEngineUpdateBehaviourStitcher,
            IGuiBehaviourStitcher guiBehaviourStitcher,
            IUnityGuiHitTester unityGuiHitTester,
            INoesisGuiHitTester noesisGuiHitTester,
            IExploreGameRootPrefabFactory exploreGameRootPrefabFactory,
            IHasFollowCameraBehaviourStitcher hasFollowCameraBehaviourStitcher,
            Lazy<IHudController> lazyHudController,
            Lazy<IInGameMenuController> lazyInGameMenuController,
            Lazy<IHudView> lazyHudView)
        {
            _gameObjectManager = gameObjectManager;
            _mapPrefabFactory = mapPrefabFactory;
            _guiInputStitcher = guiInputStitcher;
            _exploreSceneStartupInterceptorFacade = exploreSceneStartupInterceptorFacade;
            _gameEngineUpdateBehaviourStitcher = gameEngineUpdateBehaviourStitcher;
            _guiBehaviorStitcher = guiBehaviourStitcher;
            _unityGuiHitTester = unityGuiHitTester;
            _noesisGuiHitTester = noesisGuiHitTester;
            _exploreGameRootPrefabFactory = exploreGameRootPrefabFactory;
            _hasFollowCameraBehaviourStitcher = hasFollowCameraBehaviourStitcher;
            _lazyHudController = lazyHudController;
            _lazyInGameMenuController = lazyInGameMenuController;
            _lazyHudView = lazyHudView;
        }

        public void Setup()
        {
            var exploreGameRoot = _exploreGameRootPrefabFactory.GetInstance();

            _gameEngineUpdateBehaviourStitcher.Attach(exploreGameRoot.GameObject);

            _guiBehaviorStitcher.Stitch(
                exploreGameRoot.GameObject,
                x => x.activeInHierarchy && x.name == "FollowCamera",
                _lazyHudView.Value,
                x => _noesisGuiHitTester.Setup((NoesisView)x)); 
            _unityGuiHitTester.Setup(
                _gameObjectManager
                    .FindAll(x => x.name == "Canvas")
                    .Single()
                    .GetComponent<GraphicRaycaster>(),
                _gameObjectManager
                    .FindAll(x => x.name == "EventSystem")
                    .Single()
                    .GetComponent<EventSystem>());

            var consoleObject = new GameObject()
            {
                name = "ConsoleCommands",
            };
            consoleObject.AddComponent<GlobalConsoleCommandsBehaviour>();
            consoleObject.AddComponent<ConsoleCommandsBehaviour>();
            consoleObject.transform.parent = exploreGameRoot.GameObject.transform;

            var mapPrefab = _mapPrefabFactory.CreateMap("Map");
            mapPrefab.GameObject.transform.parent = exploreGameRoot.GameObject.transform;

            _guiInputStitcher.Attach(exploreGameRoot.GameObject);
            _exploreSceneStartupInterceptorFacade.Intercept(exploreGameRoot.GameObject);

            _lazyHudController.Value.CloseAllWindows();
            _lazyInGameMenuController.Value.CloseMenu();

            _hasFollowCameraBehaviourStitcher.Attach(exploreGameRoot.GameObject);
        }
    }
}
