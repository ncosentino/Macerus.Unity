using System.Collections.Generic;
using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public sealed class GameObjectBehaviorInterceptorFacade : IGameObjectBehaviorInterceptorFacade
    {
        private readonly List<IGameObjectBehaviorInterceptor> _interceptors;

        public GameObjectBehaviorInterceptorFacade()
        {
            _interceptors = new List<IGameObjectBehaviorInterceptor>();
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