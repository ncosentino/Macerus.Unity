using System.Linq;
using System.Threading;
using Assets.Scripts.Autofac;
using Assets.Scripts.Scenes.Explore.Camera;
using Assets.Scripts.Scenes.Explore.Input;
using Assets.Scripts.Scenes.Explore.Maps;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using Autofac;
using Macerus.Api.Behaviors;
using Macerus.Api.GameObjects;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Game.Interface.Mapping;
using ProjectXyz.Shared.Framework;
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

            var resourcePrefabLoader = dependencyContainer.Resolve<IResourcePrefabLoader>();

            var mapBehaviourStitcher = dependencyContainer.Resolve<IMapBehaviourStitcher>();
            var mapObject = resourcePrefabLoader.Create<GameObject>("Mapping/Prefabs/Map");
            mapObject.transform.parent = gameObject.transform;
            mapObject.name = "Map";
            mapBehaviourStitcher.Attach(mapObject);

            var mapManager = dependencyContainer.Resolve<IMapManager>();
            mapManager.SwitchMap("swamp");

            var guiInputStitcher = dependencyContainer.Resolve<IGuiInputStitcher>();
            guiInputStitcher.Attach(gameObject);

            var gameObjectRepository = dependencyContainer.Resolve<IGameObjectRepository>();
            var someActor = gameObjectRepository.Load(
                new StringIdentifier("actor"),
                new StringIdentifier("player"));
            var worldLocation = someActor.Behaviors.Get<IWorldLocationBehavior>().Single();

            var playerObject = resourcePrefabLoader.Create<GameObject>("Mapping/Prefabs/PlayerPlaceholder");
            ////playerObject.transform.parent = mapObject.transform;
            playerObject.transform.position = new Vector3(
                (float)worldLocation.X,
                (float)worldLocation.Y,
                -1);
            playerObject.name = "Player";

            var followCamera = resourcePrefabLoader.Create<GameObject>("Mapping/Prefabs/FollowCamera");
            followCamera.transform.parent = gameObject.transform;
            followCamera.name = "FollowCamera";
            var cameraTargetting = followCamera.GetRequiredComponent<ICameraTargetting>();
            cameraTargetting.SetTarget(playerObject.transform);
        }
    }
}
