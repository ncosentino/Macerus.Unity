using System.Linq;

using Assets.Scripts.Scenes.Explore.GameObjects;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public sealed class MovementBehaviorInterceptor : IDiscoverableGameObjectBehaviorInterceptor
    {
        private readonly ISyncMacerusToUnityVelocityBehaviourStitcher _syncMacerusToUnityVelocityBehaviourStitcher;

        public MovementBehaviorInterceptor(ISyncMacerusToUnityVelocityBehaviourStitcher syncMacerusToUnityVelocityBehaviourStitcher)
        {
            _syncMacerusToUnityVelocityBehaviourStitcher = syncMacerusToUnityVelocityBehaviourStitcher;
        }

        public void Intercept(
            IGameObject gameObject, 
            GameObject unityGameObject)
        {
            var movementBehavior = gameObject
                .Get<IMovementBehavior>()
                .SingleOrDefault();
            if (movementBehavior == null)
            {
                return;
            }

            _syncMacerusToUnityVelocityBehaviourStitcher.Stitch(
                gameObject,
                unityGameObject);
        }
    }
}