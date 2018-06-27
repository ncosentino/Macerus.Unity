using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public interface IHasFollowCameraBehaviourStitcher
    {
        IReadOnlyHasFollowCameraBehaviour Attach(GameObject gameObject);
    }
}