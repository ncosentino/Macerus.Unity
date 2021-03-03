using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IPlayerQuickSlotControlsBehaviourStitcher
    {
        IReadOnlyPlayerQuickSlotControlsBehaviour Attach(GameObject gameObject);
    }
}