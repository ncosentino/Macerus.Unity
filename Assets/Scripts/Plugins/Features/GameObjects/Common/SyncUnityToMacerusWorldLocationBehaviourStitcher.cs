using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public sealed class SyncUnityToMacerusWorldLocationBehaviourStitcher : ISyncUnityToMacerusWorldLocationBehaviourStitcher
    {
        public ISyncUnityToMacerusWorldLocationBehaviour Stitch(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var worldLocationBehavior = gameObject.GetOnly<IWorldLocationBehavior>();

            var syncUnityToMacerusWorldLocationBehaviour = unityGameObject.AddComponent<SyncUnityToMacerusWorldLocationBehaviour>();
            syncUnityToMacerusWorldLocationBehaviour.WorldLocationBehavior = worldLocationBehavior;

            return syncUnityToMacerusWorldLocationBehaviour;
        }
    }
}