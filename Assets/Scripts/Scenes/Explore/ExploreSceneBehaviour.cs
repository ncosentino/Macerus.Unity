using System.Threading;
using System.Threading.Tasks;
using Assets.Scripts.Behaviours;
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
        private readonly CancellationTokenSource _gameEngineCancelTokenSource;
        
        public ExploreSceneBehaviour()
        {
            _gameEngineCancelTokenSource = new CancellationTokenSource();
        }

        private void Start()
        {
            var dependencyContainer = GameDependencyBehaviour.Container;
            
            var gameEngine = dependencyContainer.Resolve<IAsyncGameEngine>();
            gameEngine.RunAsync(_gameEngineCancelTokenSource.Token);

            var mapFactory = dependencyContainer.Resolve<IMapFactory>();
            var mapObject = mapFactory.CreateMap();
            mapObject.transform.parent = gameObject.transform;
            
            var mapManager = dependencyContainer.Resolve<IMapManager>();
            mapManager.SwitchMap(new StringIdentifier("swamp"));

            var guiInputStitcher = dependencyContainer.Resolve<IGuiInputStitcher>();
            guiInputStitcher.Attach(gameObject);

            var cameraFactory = dependencyContainer.Resolve<ICameraFactory>();
            var followCamera = cameraFactory.CreateCamera();
            followCamera.transform.parent = gameObject.transform;
        }

        private void OnDestroy()
        {
            _gameEngineCancelTokenSource?.Cancel(true);
        }
    }
}
