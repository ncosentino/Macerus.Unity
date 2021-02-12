using System.Linq;

using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Scenes.Explore.Api;
using Assets.Scripts.Scenes.Explore.Input;
using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Game.Interface.Mapping;
using ProjectXyz.Shared.Framework;

namespace Assets.Scripts.Scenes.Explore
{
    public sealed class ExploreSetup : IExploreSetup
    {
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IMapFactory _mapFactory;
        private readonly IGuiInputStitcher _guiInputStitcher;
        private readonly IExploreSceneStartupInterceptorFacade _exploreSceneStartupInterceptorFacade;
        private readonly IGameEngineUpdateBehaviourStitcher _gameEngineUpdateBehaviourStitcher;
        private readonly IMapManager _mapManager;

        public ExploreSetup(
            IGameObjectManager gameObjectManager,
            IMapFactory mapFactory,
            IGuiInputStitcher guiInputStitcher,
            IExploreSceneStartupInterceptorFacade exploreSceneStartupInterceptorFacade,
            IGameEngineUpdateBehaviourStitcher gameEngineUpdateBehaviourStitcher,
            IMapManager mapManager)
        {
            _gameObjectManager = gameObjectManager;
            _mapFactory = mapFactory;
            _guiInputStitcher = guiInputStitcher;
            _exploreSceneStartupInterceptorFacade = exploreSceneStartupInterceptorFacade;
            _gameEngineUpdateBehaviourStitcher = gameEngineUpdateBehaviourStitcher;
            _mapManager = mapManager;
        }

        public void Setup()
        {
            var rootGameObject = _gameObjectManager
                .FindAll(x => x.name == "Game")
                .Single();
            _gameEngineUpdateBehaviourStitcher.Attach(rootGameObject);

            var mapObject = _mapFactory.CreateMap();
            mapObject.transform.parent = rootGameObject.transform;

            _guiInputStitcher.Attach(rootGameObject);
            _exploreSceneStartupInterceptorFacade.Intercept(rootGameObject);

            _mapManager.SwitchMap(new StringIdentifier("swamp"));
        }
    }
}
