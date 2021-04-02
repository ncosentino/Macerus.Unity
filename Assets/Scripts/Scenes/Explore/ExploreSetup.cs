using System.Linq;

using Assets.Scripts.Console;
using Assets.Scripts.Gui;
using Assets.Scripts.Gui.Noesis.Views.Resources;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Scenes.Explore.Api;
using Assets.Scripts.Scenes.Explore.Console;
using Assets.Scripts.Scenes.Explore.Input;
using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;

using UnityEngine;

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

        public ExploreSetup(
            IUnityGameObjectManager gameObjectManager,
            IMapPrefabFactory mapPrefabFactory,
            IGuiInputStitcher guiInputStitcher,
            IExploreSceneStartupInterceptorFacade exploreSceneStartupInterceptorFacade,
            IGameEngineUpdateBehaviourStitcher gameEngineUpdateBehaviourStitcher,
            IMapManager mapManager,
            IGuiBehaviourStitcher guiBehaviourStitcher)
        {
            _gameObjectManager = gameObjectManager;
            _mapPrefabFactory = mapPrefabFactory;
            _guiInputStitcher = guiInputStitcher;
            _exploreSceneStartupInterceptorFacade = exploreSceneStartupInterceptorFacade;
            _gameEngineUpdateBehaviourStitcher = gameEngineUpdateBehaviourStitcher;
            _mapManager = mapManager;
            _guiBehaviorStitcher = guiBehaviourStitcher;
        }

        public void Setup()
        {
            var rootGameObject = _gameObjectManager
                .FindAll(x => x.name == "Game")
                .Single();
            _gameEngineUpdateBehaviourStitcher.Attach(rootGameObject);
            var container = new Container(); // FIXME: replace with the actual GUI we want to use here
            _guiBehaviorStitcher.Stitch(
                rootGameObject,
                x => x.activeInHierarchy && x.name == "FollowCamera",
                container);

            var consoleObject = new GameObject()
            {
                name = "ConsoleCommands",
            };
            consoleObject.AddComponent<GlobalConsoleCommandsBehaviour>();
            consoleObject.AddComponent<ConsoleCommandsBehaviour>();
            consoleObject.transform.parent = rootGameObject.transform;

            var mapObject = _mapPrefabFactory.CreateMap();
            mapObject.transform.parent = rootGameObject.transform;

            _guiInputStitcher.Attach(rootGameObject);
            _exploreSceneStartupInterceptorFacade.Intercept(rootGameObject);

            _mapManager.SwitchMap(new StringIdentifier("swamp"));
        }
    }
}
