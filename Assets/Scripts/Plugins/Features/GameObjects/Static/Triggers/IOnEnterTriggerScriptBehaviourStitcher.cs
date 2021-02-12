
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static.Triggers
{
    public interface IOnEnterTriggerScriptBehaviourStitcher
    {
        IReadOnlyOnEnterTriggerScriptBehaviour Stitch(
            IGameObject gameObject,
            GameObject unityGameObject,
            IIdentifier scriptId);
    }
}
