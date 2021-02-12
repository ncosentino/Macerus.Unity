
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static
{
    public sealed class OnEnterTriggerScriptBehaviourStitcher : IOnEnterTriggerScriptBehaviourStitcher
    {
        public IReadOnlyOnEnterTriggerScriptBehaviour Stitch(
            IGameObject gameObject,
            GameObject unityGameObject,
            IIdentifier scriptId)
        {
            var onEnterTriggerScriptBehaviour = unityGameObject.AddComponent<OnEnterTriggerScriptBehaviour>();
            onEnterTriggerScriptBehaviour.ScriptId = scriptId;
            return onEnterTriggerScriptBehaviour;
        }
    }
}
