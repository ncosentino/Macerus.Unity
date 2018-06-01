using Assets.Scripts.Api.GameObjects;
using Assets.Scripts.Unity.Resources;
using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public sealed class UnityGameObjectRepository : IUnityGameObjectRepository
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly IMapObjectStitcher _mapObjectStitcher;

        public UnityGameObjectRepository(
            IPrefabCreator prefabCreator,
            IMapObjectStitcher mapObjectStitcher)
        {
            _prefabCreator = prefabCreator;
            _mapObjectStitcher = mapObjectStitcher;
        }

        public GameObject Create(IGameObject gameObject)
        {
            // get the prefab id
            var prefabResourceId = gameObject
                .GetOnly<IReadOnlyPrefabResourceBehavior>()
                .PrefabResourceId;

            // create the object
            var unityGameObject = _prefabCreator.Create<GameObject>(prefabResourceId);
            _mapObjectStitcher.Stitch(
                gameObject,
                unityGameObject,
                prefabResourceId);

            return unityGameObject;
        }
    }
}