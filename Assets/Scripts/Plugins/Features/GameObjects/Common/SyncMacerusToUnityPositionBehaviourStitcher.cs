using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public sealed class SyncMacerusToUnityPositionBehaviourStitcher : ISyncMacerusToUnityPositionBehaviourStitcher
    {
        private readonly IMacerusToUnityPositionSynchronizer _macerusToUnityPositionSynchronizer;

        public SyncMacerusToUnityPositionBehaviourStitcher(IMacerusToUnityPositionSynchronizer macerusToUnityPositionSynchronizer)
        {
            _macerusToUnityPositionSynchronizer = macerusToUnityPositionSynchronizer;
        }

        public void Stitch(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var syncMacerusToUnityPositionBehaviour = unityGameObject.AddComponent<SyncMacerusToUnityPositionBehaviour>();
            syncMacerusToUnityPositionBehaviour.MacerusToUnityPositionSynchronizer = _macerusToUnityPositionSynchronizer;
            syncMacerusToUnityPositionBehaviour.ObservablePositionBehavior = gameObject.GetOnly<IObservablePositionBehavior>();
            syncMacerusToUnityPositionBehaviour.ObservableSizeBehavior = gameObject.GetOnly<IObservableSizeBehavior>();
        }
    }
}