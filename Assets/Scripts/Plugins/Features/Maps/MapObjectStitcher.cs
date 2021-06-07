using Assets.Scripts.Api.GameObjects;
using Assets.Scripts.Plugins.Features.GameObjects.Common;
using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public sealed class MapObjectStitcher : IMapObjectStitcher
    {
        private readonly IPrefabStitcherFacade _prefabStitcherFacade;
        private readonly IIdentityBehaviourStitcher _identityBehaviourStitcher;
        private readonly ISyncUnityToMacerusPositionBehaviourStitcher _syncUnityToMacerusPositionBehaviourStitcher;
        private readonly ISyncMacerusToUnityPositionBehaviourStitcher _syncMacerusToUnityPositionBehaviourStitcher;
        private readonly IHasGameObjectBehaviourStitcher _hasGameObjectBehaviourStitcher;
        private readonly IGameObjectBehaviorInterceptorFacade _gameObjectBehaviorInterceptorFacade;

        public MapObjectStitcher(
            IPrefabStitcherFacade prefabStitcherFacade,
            IIdentityBehaviourStitcher identityBehaviourStitcher,
            ISyncUnityToMacerusPositionBehaviourStitcher syncUnityToMacerusPositionBehaviourStitcher,
            ISyncMacerusToUnityPositionBehaviourStitcher syncMacerusToUnityPositionBehaviourStitcher,
            IHasGameObjectBehaviourStitcher hasGameObjectBehaviourStitcher,
            IGameObjectBehaviorInterceptorFacade gameObjectBehaviorInterceptorFacade)
        {
            _prefabStitcherFacade = prefabStitcherFacade;
            _identityBehaviourStitcher = identityBehaviourStitcher;
            _syncUnityToMacerusPositionBehaviourStitcher = syncUnityToMacerusPositionBehaviourStitcher;
            _syncMacerusToUnityPositionBehaviourStitcher = syncMacerusToUnityPositionBehaviourStitcher;
            _hasGameObjectBehaviourStitcher = hasGameObjectBehaviourStitcher;
            _gameObjectBehaviorInterceptorFacade = gameObjectBehaviorInterceptorFacade;
        }

        public void Stitch(
            IGameObject gameObject,
            GameObject unityGameObject,
            IIdentifier prefabResourceId)
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
                gameObject,
                prefabResourceId);

            // handle behaviors of the game object (i.e. Backend->Unity)
            _gameObjectBehaviorInterceptorFacade.Intercept(
                gameObject,
                unityGameObject);

            // synchronize world location between unity and the backend
            _syncMacerusToUnityPositionBehaviourStitcher.Stitch(
               gameObject,
               unityGameObject);
            _syncUnityToMacerusPositionBehaviourStitcher.Stitch(
                gameObject,
                unityGameObject);
        }
    }
}