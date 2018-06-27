using System;
using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public sealed class AnimatedWeatherFactoryFacade : IAnimatedWeatherFactoryFacade
    {
        private readonly Dictionary<IIdentifier, Func<GameObject>> _mappings;

        public AnimatedWeatherFactoryFacade()
        {
            _mappings = new Dictionary<IIdentifier, Func<GameObject>>();
        }

        public GameObject Create(IIdentifier weatherResourceId)
        {
            GameObject weatherGameObject;
            Func<GameObject> createCallback;
            if (_mappings.TryGetValue(
                weatherResourceId,
                out createCallback))
            {
                weatherGameObject = createCallback.Invoke();
            }
            else
            {
                weatherGameObject = new GameObject();
            }

            weatherGameObject.name = $"Weather: {weatherResourceId}";
            return weatherGameObject;
        }

        public void Register(IIdentifier weatherResourceId, Func<GameObject> callback)
        {
            _mappings.Add(weatherResourceId, callback);
        }
    }
}