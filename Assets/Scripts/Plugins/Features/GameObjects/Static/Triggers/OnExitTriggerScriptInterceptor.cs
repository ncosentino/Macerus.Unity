using System.Linq;

using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static.Triggers
{
    public sealed class OnExitTriggerScriptInterceptor : IDiscoverableGameObjectBehaviorInterceptor
    {
        private readonly IOnExitTriggerScriptBehaviourStitcher _onExitTriggerScriptBehaviourStitcher;

        public OnExitTriggerScriptInterceptor(IOnExitTriggerScriptBehaviourStitcher onExitTriggerScriptBehaviourStitcher)
        {
            _onExitTriggerScriptBehaviourStitcher = onExitTriggerScriptBehaviourStitcher;
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

            var scriptId = triggerScriptBehavior.OnExitTriggerScriptId;
            if (scriptId == null)
            {
                return;
            }

            _onExitTriggerScriptBehaviourStitcher.Stitch(
                gameObject,
                unityGameObject,
                scriptId);
        }
    }
}
