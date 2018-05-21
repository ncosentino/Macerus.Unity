using System.Threading;
using Assets.Scripts.Autofac;
using Assets.Scripts.Scenes.Explore.Camera;
using Assets.Scripts.Scenes.Explore.Input;
using Assets.Scripts.Scenes.Explore.Maps;
using Assets.Scripts.Unity.GameObjects;
using Autofac;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Game.Interface.Mapping;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore
{
    public sealed class ExploreSceneBehaviour : MonoBehaviour
    {   
        private void Start()
        {
            var dependencyContainer = new MacerusContainerBuilder().CreateContainer();

            var gameEngine = dependencyContainer.Resolve<IGameEngine>();
            gameEngine.Start(CancellationToken.None);

            var mapBehaviourStitcher = dependencyContainer.Resolve<IMapBehaviourStitcher>();
            mapBehaviourStitcher.Attach(GameObject.Find("Map"));

            var mapManager = dependencyContainer.Resolve<IMapManager>();
            mapManager.SwitchMap("swamp");

            var guiInputStitcher = dependencyContainer.Resolve<IGuiInputStitcher>();
            guiInputStitcher.Attach(gameObject);

            var cameraTargetting = GameObject
                .Find("FollowCamera")
                .GetRequiredComponent<ICameraTargetting>();
            cameraTargetting.SetTarget(GameObject.Find("PlayerPlaceholder").transform);
        }
    }
}
