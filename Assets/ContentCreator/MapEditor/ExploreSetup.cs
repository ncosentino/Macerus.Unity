using Assets.ContentCreator.MapEditor;
using Assets.Scripts.Plugins.Features.Console;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Scenes.Explore.Api;
using Assets.Scripts.Scenes.Explore.Console;

using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor
{
    public sealed class ExploreSetup : IExploreSetup
    {
        private readonly IMapPrefabFactory _mapPrefabFactory;
        private readonly IExploreSceneStartupInterceptorFacade _exploreSceneStartupInterceptorFacade;
        private readonly IGameEngineUpdateBehaviourStitcher _gameEngineUpdateBehaviourStitcher;
        private readonly IMapManager _mapManager;
        private readonly IMapFormatter _mapFormatter;
        private readonly IExploreGameRootPrefabFactory _exploreGameRootPrefabFactory;

        public ExploreSetup(
            IMapPrefabFactory mapPrefabFactory,
            IExploreSceneStartupInterceptorFacade exploreSceneStartupInterceptorFacade,
            IGameEngineUpdateBehaviourStitcher gameEngineUpdateBehaviourStitcher,
            IMapManager mapManager,
            IMapFormatter mapFormatter,
            IExploreGameRootPrefabFactory exploreGameRootPrefabFactory)
        {
            _mapPrefabFactory = mapPrefabFactory;
            _exploreSceneStartupInterceptorFacade = exploreSceneStartupInterceptorFacade;
            _gameEngineUpdateBehaviourStitcher = gameEngineUpdateBehaviourStitcher;
            _mapManager = mapManager;
            _mapFormatter = mapFormatter;
            _exploreGameRootPrefabFactory = exploreGameRootPrefabFactory;
        }

        public void Setup()
        {
            var exploreGameRoot = _exploreGameRootPrefabFactory.GetInstance();

            _gameEngineUpdateBehaviourStitcher.Attach(exploreGameRoot.GameObject);

            var consoleObject = new GameObject()
            {
                name = "ConsoleCommands",
            };
            consoleObject.AddComponent<GlobalConsoleCommandsBehaviour>();
            consoleObject.AddComponent<ConsoleCommandsBehaviour>();
            consoleObject.transform.parent = exploreGameRoot.GameObject.transform;

            var mapPrefab = _mapPrefabFactory.CreateMap("Map");
            mapPrefab.GameObject.transform.parent = exploreGameRoot.GameObject.transform;

            _exploreSceneStartupInterceptorFacade.Intercept(exploreGameRoot.GameObject);

            // FIXME: this is just for testing
            _mapManager.SwitchMap(new StringIdentifier("swamp"));
            _mapFormatter.ToggleGridLines(true);
        }
    }
}
