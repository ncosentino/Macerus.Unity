using System.Linq;

using Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours;
using Assets.Scripts.Scenes.Explore.GameObjects;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Animations.Interceptors
{
    public sealed class SpriteAnimationBehaviorInterceptor : IGameObjectBehaviorInterceptor
    {
        private readonly ISpriteAnimationBehaviourStitcher _spriteAnimationBehaviourStitcher;

        public SpriteAnimationBehaviorInterceptor(ISpriteAnimationBehaviourStitcher spriteAnimationBehaviourStitcher)
        {
            _spriteAnimationBehaviourStitcher = spriteAnimationBehaviourStitcher;
        }

        public void Intercept(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var animationBehavior = gameObject.GetOnly<IAnimationBehavior>();
            if (animationBehavior == null)
            {
                return;
            }

            _spriteAnimationBehaviourStitcher.Attach(
                unityGameObject,
                animationBehavior);
        }
    }
}