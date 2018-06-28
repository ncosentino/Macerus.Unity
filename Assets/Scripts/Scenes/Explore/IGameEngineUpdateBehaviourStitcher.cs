using UnityEngine;

namespace Assets.Scripts.Scenes.Explore
{
    public interface IGameEngineUpdateBehaviourStitcher
    {
        IReadOnlyGameEngineUpdateBehaviour Attach(GameObject gameObject);
    }
}