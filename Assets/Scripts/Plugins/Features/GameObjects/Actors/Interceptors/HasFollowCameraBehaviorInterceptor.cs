using System.Linq;
using Assets.Scripts.Api.GameObjects;
using Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours;
using Assets.Scripts.Scenes.Explore.GameObjects;
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
            // this is one seriously crazy hack right now... do we need the
            // back -end to know about follow cameras? maybe in an abstract sense?
            var hasSomethingThatTellsUsItsThePlayer = gameObject
                .Get<IPrefabResourceBehavior>()
                .SingleOrDefault(x => x
                .PrefabResourceId
                .ToString()
                .Equals("Mapping/Prefabs/PlayerPlaceholder", System.StringComparison.OrdinalIgnoreCase));
            if (hasSomethingThatTellsUsItsThePlayer == null)
            {
                return;
            }

            _hasFollowCameraBehaviourStitcher.Attach(unityGameObject);
        }
    }
}