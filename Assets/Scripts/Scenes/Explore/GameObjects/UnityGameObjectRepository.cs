using System.Linq;
using Assets.Scripts.Plugins.Features.GameObjects.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public sealed class UnityGameObjectRepository : IUnityGameObjectRepository
    {
        private readonly IPrefabCreator _prefabCreator;

        public UnityGameObjectRepository(
            IPrefabCreator prefabCreator)
        {
            _prefabCreator = prefabCreator;
        }

        public GameObject Create(IGameObject gameObject)
        {
            var prefabResourceBehavior = gameObject
                .Behaviors.Get<IReadOnlyPrefabResourceBehavior>()
                .Single();
            var unityGameObject = _prefabCreator.Create<GameObject>(prefabResourceBehavior.PrefabResourceId);

            // set an ID on the game object
            var gameObjectId = gameObject
                .Behaviors
                .Get<IIdentifierBehavior>()
                .Single()
                .Id;
            unityGameObject.GetRequiredComponent<IIdentifierBehaviour>().Id = gameObjectId;
            unityGameObject.name = $"GameObject {gameObjectId}";

            return unityGameObject;
        }
    }
}