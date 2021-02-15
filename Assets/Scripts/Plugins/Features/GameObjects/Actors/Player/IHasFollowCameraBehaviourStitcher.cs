
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IHasFollowCameraBehaviourStitcher
    {
        IReadOnlyHasFollowCameraBehaviour Attach(GameObject gameObject);
    }
}