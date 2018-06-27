using System.Threading;
using System.Threading.Tasks;
using Assets.Scripts.Behaviours;
using Assets.Scripts.Scenes.Explore.Api;
using Assets.Scripts.Scenes.Explore.Camera;
using Assets.Scripts.Scenes.Explore.Input;
using Assets.Scripts.Scenes.Explore.Maps;
using Autofac;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Game.Interface.Mapping;
using ProjectXyz.Shared.Framework;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore
{
    public sealed class ExploreSceneBehaviour : MonoBehaviour
    {
        private IGameEngine _gameEngine;

        private void Start()
        {
            var dependencyContainer = GameDependencyBehaviour.Container;
            
            _gameEngine = dependencyContainer.Resolve<IGameEngine>();

            var mapFactory = dependencyContainer.Resolve<IMapFactory>();
            var mapObject = mapFactory.CreateMap();
            mapObject.transform.parent = gameObject.transform;
            
            var mapManager = dependencyContainer.Resolve<IMapManager>();
            mapManager.SwitchMap(new StringIdentifier("swamp"));

            var guiInputStitcher = dependencyContainer.Resolve<IGuiInputStitcher>();
            guiInputStitcher.Attach(gameObject);

            var exploreSceneStartupInterceptor = dependencyContainer.Resolve<IExploreSceneStartupInterceptorFacade>();
            exploreSceneStartupInterceptor.Intercept(gameObject);
        }

        private void Update()
        {
            _gameEngine.Update();
        }
    }
}
