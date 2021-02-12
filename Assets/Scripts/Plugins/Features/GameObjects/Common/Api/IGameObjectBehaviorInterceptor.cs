using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common.Api
{
    public interface IGameObjectBehaviorInterceptor
    {
        void Intercept(
            IGameObject gameObject,
            GameObject unityGameObject);
    }
}