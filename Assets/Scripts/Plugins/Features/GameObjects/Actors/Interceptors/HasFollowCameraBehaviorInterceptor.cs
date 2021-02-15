using System.Linq;

using Assets.Scripts.Plugins.Features.GameObjects.Actors.Player;
using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public sealed class HasFollowCameraBehaviorInterceptor : IDiscoverableGameObjectBehaviorInterceptor
    {
        private readonly IHasFollowCameraBehaviourStitcher _hasFollowCameraBehaviourStitcher;

        public HasFollowCameraBehaviorInterceptor(IHasFollowCameraBehaviourStitcher hasFollowCameraBehaviourStitcher)
        {
            _hasFollowCameraBehaviourStitcher = hasFollowCameraBehaviourStitcher;
        }

        public void Intercept(
            IGameObject gameObject, 
            GameObject unityGameObject)
        {
            var playerControlled = gameObject
                .Get<IPlayerControlledBehavior>()
                .Any();
            if (!playerControlled)
            {
                return;
            }

            _hasFollowCameraBehaviourStitcher.Attach(unityGameObject);
        }
    }
}