using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Unity.Threading;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public sealed class SyncMacerusToUnityPositionBehaviourStitcher : ISyncMacerusToUnityPositionBehaviourStitcher
    {
        private readonly IMacerusToUnityPositionSynchronizer _macerusToUnityPositionSynchronizer;
        private readonly IDispatcher _dispatcher;

        public SyncMacerusToUnityPositionBehaviourStitcher(
            IMacerusToUnityPositionSynchronizer macerusToUnityPositionSynchronizer,
            IDispatcher dispatcher)
        {
            _macerusToUnityPositionSynchronizer = macerusToUnityPositionSynchronizer;
            _dispatcher = dispatcher;
        }

        public void Stitch(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var syncMacerusToUnityPositionBehaviour = unityGameObject.AddComponent<SyncMacerusToUnityPositionBehaviour>();
            syncMacerusToUnityPositionBehaviour.MacerusToUnityPositionSynchronizer = _macerusToUnityPositionSynchronizer;
            syncMacerusToUnityPositionBehaviour.Dispatcher = _dispatcher;
            syncMacerusToUnityPositionBehaviour.ObservablePositionBehavior = gameObject.GetOnly<IObservablePositionBehavior>();
            syncMacerusToUnityPositionBehaviour.ObservableSizeBehavior = gameObject.GetOnly<IObservableSizeBehavior>();
        }
    }
}