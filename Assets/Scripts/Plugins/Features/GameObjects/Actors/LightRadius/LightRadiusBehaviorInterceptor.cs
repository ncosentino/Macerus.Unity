using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public sealed class LightRadiusBehaviorInterceptor : IDiscoverableGameObjectBehaviorInterceptor
    {
        private readonly ILightRadiusStitcher _lightRadiusStitcher;

        public LightRadiusBehaviorInterceptor(ILightRadiusStitcher lightRadiusStitcher)
        {
            _lightRadiusStitcher = lightRadiusStitcher;
        }

        public void Intercept(
            IGameObject gameObject, 
            GameObject unityGameObject)
        {
            if (!gameObject.Has<IHasStatsBehavior>())
            {
                return;
            }

            _lightRadiusStitcher.Stitch(gameObject, unityGameObject);
        }
    }
}