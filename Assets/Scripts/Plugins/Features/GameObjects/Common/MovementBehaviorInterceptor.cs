using System.Linq;

using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
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