using System.Linq;
using System.Threading;
using Assets.Scripts.Behaviours;
using Assets.Scripts.Scenes.Explore.Camera;
using Assets.Scripts.Scenes.Explore.GameObjects;
using Assets.Scripts.Scenes.Explore.Input;
using Assets.Scripts.Scenes.Explore.Maps;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using Autofac;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Game.Interface.Mapping;
using ProjectXyz.Shared.Framework;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore
{
    public sealed class ExploreSceneBehaviour : MonoBehaviour
    {
        private bool _runOnce;
        private IPrefabCreator _prefabCreator;

        private void Start()
        {
            var dependencyContainer = GameDependencyBehaviour.Container;

            var gameEngine = dependencyContainer.Resolve<IGameEngine>();
            gameEngine.Start(CancellationToken.None);

            _prefabCreator = dependencyContainer.Resolve<IPrefabCreator>();

            var mapBehaviourStitcher = dependencyContainer.Resolve<IMapBehaviourStitcher>();
            var mapObject = _prefabCreator.Create<GameObject>("Mapping/Prefabs/Map");
            mapObject.transform.parent = gameObject.transform;
            mapObject.name = "Map";
            mapBehaviourStitcher.Attach(mapObject);

            var mapManager = dependencyContainer.Resolve<IMapManager>();
            mapManager.SwitchMap(new StringIdentifier("swamp"));

            var guiInputStitcher = dependencyContainer.Resolve<IGuiInputStitcher>();
            guiInputStitcher.Attach(gameObject);
        }

        private void Update()
        {
            if (_runOnce)
            {
                return;
            }

            var playerObject = gameObject
                .GetComponentsInChildren<IdentifierBehaviour>()
                .SingleOrDefault(x => x.Id.Equals(new StringIdentifier("player")))
                ?.gameObject;
            if (playerObject == null)
            {
                return;
            }

            Debug.Log("Found player on map...");
            var followCamera = _prefabCreator.Create<GameObject>("Mapping/Prefabs/FollowCamera");
            followCamera.transform.parent = gameObject.transform;
            followCamera.name = "FollowCamera";

            var cameraTargetting = followCamera.GetRequiredComponent<ICameraTargetting>();
            cameraTargetting.SetTarget(playerObject.transform);

            _runOnce = true;
        }
    }
}
