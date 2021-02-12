
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static
{
    public interface IOnEnterTriggerScriptBehaviourStitcher
    {
        IReadOnlyOnEnterTriggerScriptBehaviour Stitch(
            IGameObject gameObject,
            GameObject unityGameObject,
            IIdentifier scriptId);
    }
}
