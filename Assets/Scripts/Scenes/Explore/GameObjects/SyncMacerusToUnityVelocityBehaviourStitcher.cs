using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public sealed class SyncMacerusToUnityVelocityBehaviourStitcher : ISyncMacerusToUnityVelocityBehaviourStitcher
    {
        public ISyncMacerusToUnityVelocityBehaviour Stitch(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var movementBehavior = gameObject.GetOnly<IMovementBehavior>();
            var rigidbody2d = unityGameObject.GetComponent<Rigidbody2D>();

            var syncMacerusThrottleToUnityBehaviour = unityGameObject.AddComponent<SyncMacerusToUnityVelocityBehaviour>();
            syncMacerusThrottleToUnityBehaviour.ObservableMovementBehavior = movementBehavior;
            syncMacerusThrottleToUnityBehaviour.RigidBody2D = rigidbody2d;

            return syncMacerusThrottleToUnityBehaviour;
        }
    }
}