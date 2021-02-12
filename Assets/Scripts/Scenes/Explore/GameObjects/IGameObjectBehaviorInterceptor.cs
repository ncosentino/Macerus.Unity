using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IGameObjectBehaviorInterceptor
    {
        void Intercept(
            IGameObject gameObject,
            GameObject unityGameObject);
    }
}