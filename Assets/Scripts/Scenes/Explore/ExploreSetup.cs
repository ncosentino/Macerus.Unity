using System.Linq;

using Assets.Scripts.Gui;
using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Gui.Noesis.Views.Resources;
using Assets.Scripts.Gui.Unity;
using Assets.Scripts.Plugins.Features.Audio.Api;
using Assets.Scripts.Plugins.Features.Console;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Scenes.Explore.Api;
using Assets.Scripts.Scenes.Explore.Console;
using Assets.Scripts.Scenes.Explore.Input;
using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;

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
        private readonly IMapManager _mapManager;
        private readonly IGuiBehaviourStitcher _guiBehaviorStitcher;
        private readonly ISoundPlayingBehaviourStitcher _soundPlayingBehaviourStitcher;
        private readonly IUnityGuiHitTester _unityGuiHitTester;
        private readonly INoesisGuiHitTester _noesisGuiHitTester;
        private readonly IExploreGameRootPrefabFactory _exploreGameRootPrefabFactory;

        public ExploreSetup(
            IUnityGameObjectManager gameObjectManager,
            IMapPrefabFactory mapPrefabFactory,
            IGuiInputStitcher guiInputStitcher,
            IExploreSceneStartupInterceptorFacade exploreSceneStartupInterceptorFacade,
            IGameEngineUpdateBehaviourStitcher gameEngineUpdateBehaviourStitcher,
            IMapManager mapManager,
            IGuiBehaviourStitcher guiBehaviourStitcher,
            ISoundPlayingBehaviourStitcher soundPlayingBehaviourStitcher,
            IUnityGuiHitTester unityGuiHitTester,
            INoesisGuiHitTester noesisGuiHitTester,
            IExploreGameRootPrefabFactory exploreGameRootPrefabFactory)
        {
            _gameObjectManager = gameObjectManager;
            _mapPrefabFactory = mapPrefabFactory;
            _guiInputStitcher = guiInputStitcher;
            _exploreSceneStartupInterceptorFacade = exploreSceneStartupInterceptorFacade;
            _gameEngineUpdateBehaviourStitcher = gameEngineUpdateBehaviourStitcher;
            _mapManager = mapManager;
            _guiBehaviorStitcher = guiBehaviourStitcher;
            _soundPlayingBehaviourStitcher = soundPlayingBehaviourStitcher;
            _unityGuiHitTester = unityGuiHitTester;
            _noesisGuiHitTester = noesisGuiHitTester;
            _exploreGameRootPrefabFactory = exploreGameRootPrefabFactory;
        }

        public void Setup()
        {
            var exploreGameRoot = _exploreGameRootPrefabFactory.GetInstance();

            _gameEngineUpdateBehaviourStitcher.Attach(exploreGameRoot.GameObject);
            _soundPlayingBehaviourStitcher.Attach(exploreGameRoot.GameObject);

            var view = new Container(); // FIXME: replace with the actual GUI we want to use here;
            _guiBehaviorStitcher.Stitch(
                exploreGameRoot.GameObject,
                x => x.activeInHierarchy && x.name == "FollowCamera",
                view,
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

            // FIXME: this is just for testing
            _mapManager.SwitchMap(new StringIdentifier("swamp"));
        }
    }
}
