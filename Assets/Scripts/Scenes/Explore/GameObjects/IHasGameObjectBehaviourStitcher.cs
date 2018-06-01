using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IHasGameObjectBehaviourStitcher
    {
        IHasGameObject Attach(
            IGameObject gameObject,
            GameObject unityGameObject);
    }
}