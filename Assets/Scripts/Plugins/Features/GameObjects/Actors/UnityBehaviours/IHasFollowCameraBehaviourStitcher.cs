using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public interface IHasFollowCameraBehaviourStitcher
    {
        IReadOnlyHasFollowCameraBehaviour Attach(GameObject gameObject);
    }
}