using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public sealed class SyncUnityToMacerusWorldLocationBehaviourStitcher : ISyncUnityToMacerusWorldLocationBehaviourStitcher
    {
        private readonly IMacerusToUnityWorldLocationSynchronizer _macerusToUnityWorldLocationSynchronizer;

        public SyncUnityToMacerusWorldLocationBehaviourStitcher(IMacerusToUnityWorldLocationSynchronizer macerusToUnityWorldLocationSynchronizer)
        {
            _macerusToUnityWorldLocationSynchronizer = macerusToUnityWorldLocationSynchronizer;
        }

        public ISyncUnityToMacerusWorldLocationBehaviour Stitch(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var worldLocationBehavior = gameObject.GetOnly<IWorldLocationBehavior>();

            var syncUnityToMacerusWorldLocationBehaviour = unityGameObject.AddComponent<SyncUnityToMacerusWorldLocationBehaviour>();
            syncUnityToMacerusWorldLocationBehaviour.MacerusToUnityWorldLocationSynchronizer = _macerusToUnityWorldLocationSynchronizer;
            syncUnityToMacerusWorldLocationBehaviour.WorldLocationBehavior = worldLocationBehavior;

            return syncUnityToMacerusWorldLocationBehaviour;
        }
    }
}