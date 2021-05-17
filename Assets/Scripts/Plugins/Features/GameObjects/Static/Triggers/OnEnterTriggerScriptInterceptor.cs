using System.Linq;

using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static.Triggers
{
    public sealed class OnEnterTriggerScriptInterceptor : IDiscoverableGameObjectBehaviorInterceptor
    {
        private readonly IOnEnterTriggerScriptBehaviourStitcher _onEnterTriggerScriptBehaviourStitcher;

        public OnEnterTriggerScriptInterceptor(IOnEnterTriggerScriptBehaviourStitcher onEnterTriggerScriptBehaviourStitcher)
        {
            _onEnterTriggerScriptBehaviourStitcher = onEnterTriggerScriptBehaviourStitcher;
        }

        public void Intercept(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var triggerScriptBehavior = gameObject
                .Get<ITriggerScriptBehavior>()
                .SingleOrDefault();
            if (triggerScriptBehavior == null)
            {
                return;
            }

            var scriptId = triggerScriptBehavior.OnEnterTriggerScriptId;
            if (scriptId == null)
            {
                return;
            }

            _onEnterTriggerScriptBehaviourStitcher.Stitch(
                gameObject,
                unityGameObject,
                scriptId);
        }
    }
}
