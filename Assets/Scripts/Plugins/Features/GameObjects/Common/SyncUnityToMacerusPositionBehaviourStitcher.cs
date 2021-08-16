using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public sealed class SyncUnityToMacerusPositionBehaviourStitcher : ISyncUnityToMacerusPositionBehaviourStitcher
    {
        private readonly IMacerusToUnityPositionSynchronizer _macerusToUnityPositionSynchronizer;

        public SyncUnityToMacerusPositionBehaviourStitcher(IMacerusToUnityPositionSynchronizer macerusToUnityPositionSynchronizer)
        {
            _macerusToUnityPositionSynchronizer = macerusToUnityPositionSynchronizer;
        }

        public void Stitch(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var worldLocationBehavior = gameObject.GetOnly<IPositionBehavior>();

            var syncUnityToMacerusWorldLocationBehaviour = unityGameObject.AddComponent<SyncUnityToMacerusPositionBehaviour>();
            syncUnityToMacerusWorldLocationBehaviour.MacerusToUnityPositionSynchronizer = _macerusToUnityPositionSynchronizer;
            syncUnityToMacerusWorldLocationBehaviour.PositionBehavior = worldLocationBehavior;
        }
    }
}