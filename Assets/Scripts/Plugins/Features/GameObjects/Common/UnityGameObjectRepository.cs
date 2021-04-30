using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Unity.Resources.Prefabs;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
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
                .GetOnly<IReadOnlyPrefabResourceIdBehavior>()
                .PrefabResourceId;
            
            // FIXME: this is a hack. we probably could use some resource ID to path mapping here
            var relativePrefabPathWithinResources = prefabResourceId.ToString();

            // create the object
            var unityGameObject = _prefabCreator.Create<GameObject>(relativePrefabPathWithinResources);
            _mapObjectStitcher.Stitch(
                gameObject,
                unityGameObject,
                prefabResourceId);

            return unityGameObject;
        }
    }
}