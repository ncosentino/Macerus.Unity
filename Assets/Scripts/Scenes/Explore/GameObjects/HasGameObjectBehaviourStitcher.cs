using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public sealed class HasGameObjectBehaviourStitcher : IHasGameObjectBehaviourStitcher
    {
        public IHasGameObject Attach(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var hasGameObjectBehaviour = unityGameObject.AddComponent<HasGameObjectBehaviour>();
            hasGameObjectBehaviour.GameObject = gameObject;
            return hasGameObjectBehaviour;
        }
    }
}