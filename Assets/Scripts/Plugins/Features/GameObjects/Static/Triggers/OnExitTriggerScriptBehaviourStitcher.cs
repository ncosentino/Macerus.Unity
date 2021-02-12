
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static.Triggers
{
    public sealed class OnExitTriggerScriptBehaviourStitcher : IOnExitTriggerScriptBehaviourStitcher
    {
        public IReadOnlyOnExitTriggerScriptBehaviour Stitch(
            IGameObject gameObject,
            GameObject unityGameObject,
            IIdentifier scriptId)
        {
            var onExitTriggerScriptBehaviour = unityGameObject.AddComponent<OnExitTriggerScriptBehaviour>();
            onExitTriggerScriptBehaviour.ScriptId = scriptId;
            return onExitTriggerScriptBehaviour;
        }
    }
}
