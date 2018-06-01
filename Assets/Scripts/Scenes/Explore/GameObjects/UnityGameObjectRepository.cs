using System.Linq;
using Assets.Scripts.Api.GameObjects;
using Assets.Scripts.Plugins.Features.GameObjects.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using Macerus.Api.Behaviors;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public sealed class UnityGameObjectRepository : IUnityGameObjectRepository
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly IPrefabStitcherFacade _prefabStitcherFacade;

        public UnityGameObjectRepository(
            IPrefabCreator prefabCreator,
            IPrefabStitcherFacade prefabStitcherFacade)
        {
            _prefabCreator = prefabCreator;
            _prefabStitcherFacade = prefabStitcherFacade;
        }

        public GameObject Create(IGameObject gameObject)
        {
            // get the prefab id
            var prefabResourceBehavior = gameObject
                .Behaviors.Get<IReadOnlyPrefabResourceBehavior>()
                .Single();
            var prefabResourceId = prefabResourceBehavior.PrefabResourceId;

            // create the object
            var unityGameObject = _prefabCreator.Create<GameObject>(prefabResourceId);
            _prefabStitcherFacade.Stitch(
                unityGameObject,
                prefabResourceId);

            // set an ID on the game object
            var gameObjectId = gameObject
                .Behaviors
                .Get<IIdentifierBehavior>()
                .Single()
                .Id;
            unityGameObject.GetRequiredComponent<IIdentifierBehaviour>().Id = gameObjectId;
            unityGameObject.name = $"GameObject {gameObjectId}";

            // move the game object to the right spot
            var worldLocation = gameObject
                .Behaviors
                .Get<IWorldLocationBehavior>()
                .Single();
            unityGameObject.transform.position = new Vector3(
                (float)worldLocation.X,
                (float)worldLocation.Y,
                -1);

            return unityGameObject;
        }
    }
}