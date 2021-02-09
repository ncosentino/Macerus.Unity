using System.Linq;

using Assets.Scripts.Api.GameObjects;
using Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours;
using Assets.Scripts.Scenes.Explore.GameObjects;

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
            // FIXME: look for a solid backend component that indicates animation
            var hasAnimationSupport = gameObject
                .Get<IPrefabResourceBehavior>()
                .Any(x => x.PrefabResourceId == "Mapping/Prefabs/PlayerPlaceholder");
            if (!hasAnimationSupport)
            {
                return;
            }

            _spriteAnimationBehaviourStitcher.Attach(unityGameObject);
        }
    }
}