using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Scenes.Explore.Api;
using UnityEngine;

namespace Assets.ContentCreator.MapEditor
{
    public sealed class ExploreSceneStartupInterceptorFacade : IExploreSceneStartupInterceptorFacade
    {
        private readonly IReadOnlyCollection<IExploreSceneStartupInterceptor> _interceptors;

        public ExploreSceneStartupInterceptorFacade(IEnumerable<IExploreSceneStartupInterceptor> interceptors)
        {
            _interceptors = interceptors.ToArray();
        }

        public void Intercept(GameObject explore)
        {
            foreach (var interceptor in _interceptors)
            {
                interceptor.Intercept(explore);
            }
        }
    }
}