using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
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