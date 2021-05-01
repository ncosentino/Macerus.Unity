using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public sealed class SyncMacerusToUnityWorldLocationBehaviourStitcher : ISyncMacerusToUnityWorldLocationBehaviourStitcher
    {
        private readonly IMacerusToUnityWorldLocationSynchronizer _macerusToUnityWorldLocationSynchronizer;

        public SyncMacerusToUnityWorldLocationBehaviourStitcher(IMacerusToUnityWorldLocationSynchronizer macerusToUnityWorldLocationSynchronizer)
        {
            _macerusToUnityWorldLocationSynchronizer = macerusToUnityWorldLocationSynchronizer;
        }

        public ISyncMacerusToUnityWorldLocationBehaviour Stitch(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var worldLocationBehavior = gameObject.GetOnly<IObservableWorldLocationBehavior>();

            var syncMacerusToUnityWorldLocationBehaviour = unityGameObject.AddComponent<SyncMacerusToUnityWorldLocationBehaviour>();
            syncMacerusToUnityWorldLocationBehaviour.MacerusToUnityWorldLocationSynchronizer = _macerusToUnityWorldLocationSynchronizer;
            syncMacerusToUnityWorldLocationBehaviour.ObservableWorldLocationBehavior = worldLocationBehavior;

            return syncMacerusToUnityWorldLocationBehaviour;
        }
    }
}