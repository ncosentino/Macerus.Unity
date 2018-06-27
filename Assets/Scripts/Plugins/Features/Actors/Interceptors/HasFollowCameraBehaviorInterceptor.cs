using System.Linq;
using Assets.Scripts.Api.GameObjects;
using Assets.Scripts.Plugins.Features.Actors.UnityBehaviours;
using Assets.Scripts.Scenes.Explore.GameObjects;
using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.Interceptors
{
    public sealed class HasFollowCameraBehaviorInterceptor : IGameObjectBehaviorInterceptor
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
                .SingleOrDefault(x => x.PrefabResourceId == "Mapping/Prefabs/PlayerPlaceholder");
            if (hasSomethingThatTellsUsItsThePlayer == null)
            {
                return;
            }

            _hasFollowCameraBehaviourStitcher.Attach(unityGameObject);
        }
    }
}