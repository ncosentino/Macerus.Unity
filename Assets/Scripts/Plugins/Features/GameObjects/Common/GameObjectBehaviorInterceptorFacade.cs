using System.Collections.Generic;

using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public sealed class GameObjectBehaviorInterceptorFacade : IGameObjectBehaviorInterceptorFacade
    {
        private readonly List<IGameObjectBehaviorInterceptor> _interceptors;

        public GameObjectBehaviorInterceptorFacade(IEnumerable<IDiscoverableGameObjectBehaviorInterceptor> interceptors)
        {
            _interceptors = new List<IGameObjectBehaviorInterceptor>(interceptors);
        }

        public void Intercept(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            foreach (var gameObjectBehaviorInterceptor in _interceptors)
            {
                gameObjectBehaviorInterceptor.Intercept(
                    gameObject,
                    unityGameObject);
            }
        }

        public void Register(IGameObjectBehaviorInterceptor gameObjectBehaviorInterceptor)
        {
            _interceptors.Add(gameObjectBehaviorInterceptor);
        }
    }
}