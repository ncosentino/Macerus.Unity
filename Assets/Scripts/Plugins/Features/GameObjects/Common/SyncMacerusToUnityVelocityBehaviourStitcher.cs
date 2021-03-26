using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Unity.Threading;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public sealed class SyncMacerusToUnityVelocityBehaviourStitcher : ISyncMacerusToUnityVelocityBehaviourStitcher
    {
        private readonly IDispatcher _dispatcher;

        public SyncMacerusToUnityVelocityBehaviourStitcher(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public ISyncMacerusToUnityVelocityBehaviour Stitch(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var movementBehavior = gameObject.GetOnly<IMovementBehavior>();
            var rigidbody2d = unityGameObject.GetComponent<Rigidbody2D>();

            var syncMacerusThrottleToUnityBehaviour = unityGameObject.AddComponent<SyncMacerusToUnityVelocityBehaviour>();
            syncMacerusThrottleToUnityBehaviour.ObservableMovementBehavior = movementBehavior;
            syncMacerusThrottleToUnityBehaviour.RigidBody2D = rigidbody2d;
            syncMacerusThrottleToUnityBehaviour.Dispatcher = _dispatcher;

            return syncMacerusThrottleToUnityBehaviour;
        }
    }
}