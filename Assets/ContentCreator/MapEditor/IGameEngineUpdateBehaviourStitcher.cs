using UnityEngine;

namespace Assets.ContentCreator.MapEditor
{
    public interface IGameEngineUpdateBehaviourStitcher
    {
        IReadOnlyGameEngineUpdateBehaviour Attach(GameObject gameObject);
    }
}