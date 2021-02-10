using Assets.Scripts.Api.GameObjects;
using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public sealed class MapObjectStitcher : IMapObjectStitcher
    {
        private readonly IPrefabStitcherFacade _prefabStitcherFacade;
        private readonly IIdentityBehaviourStitcher _identityBehaviourStitcher;
        private readonly ISyncMacerusToUnityVelocityBehaviourStitcher _syncMacerusToUnityVelocityBehaviourStitcher;
        private readonly ISyncUnityToMacerusWorldLocationBehaviourStitcher _syncUnityToMacerusWorldLocationBehaviourStitcher;
        private readonly IHasGameObjectBehaviourStitcher _hasGameObjectBehaviourStitcher;
        private readonly IGameObjectBehaviorInterceptorFacade _gameObjectBehaviorInterceptorFacade;

        public MapObjectStitcher(
            IPrefabStitcherFacade prefabStitcherFacade,
            IIdentityBehaviourStitcher identityBehaviourStitcher,
            ISyncMacerusToUnityVelocityBehaviourStitcher syncMacerusToUnityVelocityBehaviourStitcher,
            ISyncUnityToMacerusWorldLocationBehaviourStitcher syncUnityToMacerusWorldLocationBehaviourStitcher,
            IHasGameObjectBehaviourStitcher hasGameObjectBehaviourStitcher,
            IGameObjectBehaviorInterceptorFacade gameObjectBehaviorInterceptorFacade)
        {
            _prefabStitcherFacade = prefabStitcherFacade;
            _identityBehaviourStitcher = identityBehaviourStitcher;
            _syncMacerusToUnityVelocityBehaviourStitcher = syncMacerusToUnityVelocityBehaviourStitcher;
            _syncUnityToMacerusWorldLocationBehaviourStitcher = syncUnityToMacerusWorldLocationBehaviourStitcher;
            _hasGameObjectBehaviourStitcher = hasGameObjectBehaviourStitcher;
            _gameObjectBehaviorInterceptorFacade = gameObjectBehaviorInterceptorFacade;
        }

        public void Stitch(
            IGameObject gameObject,
            GameObject unityGameObject,
            string prefabResourceId)
        {
            // every map object needs a reference to the underlying game object
            _hasGameObjectBehaviourStitcher.Attach(
                gameObject,
                unityGameObject);

            // set an ID on the game object
            _identityBehaviourStitcher.Stitch(
                gameObject,
                unityGameObject);

            // stitch additional behaviours specific to the prefab (i.e. Unity->Unity)
            _prefabStitcherFacade.Stitch(
                unityGameObject,
                prefabResourceId);

            // handle behaviors of the game object (i.e. Backend->Unity)
            _gameObjectBehaviorInterceptorFacade.Intercept(
                gameObject,
                unityGameObject);

            // synchronize velocity between unity and the backend
            _syncMacerusToUnityVelocityBehaviourStitcher.Stitch(
                gameObject,
                unityGameObject);

            // synchronize world location between unity and the backend
            _syncUnityToMacerusWorldLocationBehaviourStitcher.Stitch(
                gameObject,
                unityGameObject);
        }
    }
}