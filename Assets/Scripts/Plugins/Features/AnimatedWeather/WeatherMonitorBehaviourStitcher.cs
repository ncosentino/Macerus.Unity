using System;
using Assets.Scripts.Plugins.Features.AnimatedWeather.Api;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class WeatherMonitorBehaviourStitcher : IWeatherMonitorBehaviourStitcher
    {
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly IAnimatedWeatherFactory _animatedWeatherFactory;
        private readonly ILogger _logger;
        private readonly IWeatherProvider _weatherProvider;

        public WeatherMonitorBehaviourStitcher(
            IWeatherProvider weatherProvider,
            IObjectDestroyer objectDestroyer,
            IAnimatedWeatherFactory animatedWeatherFactory,
            ILogger logger)
        {
            _weatherProvider = weatherProvider;
            _objectDestroyer = objectDestroyer;
            _animatedWeatherFactory = animatedWeatherFactory;
            _logger = logger;
        }

        public IReadOnlyWeatherMonitorBehaviour Attach(GameObject weatherSystemGameObject)
        {
            var weatherMonitorBehaviour = weatherSystemGameObject.AddComponent<WeatherMonitorBehaviour>();
            weatherMonitorBehaviour.ObjectDestroyer = _objectDestroyer;
            weatherMonitorBehaviour.WeatherProvider = _weatherProvider;
            weatherMonitorBehaviour.Logger = _logger;
            weatherMonitorBehaviour.AnimatedWeatherFactory = _animatedWeatherFactory;

            weatherMonitorBehaviour.UpdateDelay = TimeSpan.FromSeconds(0.25);
            weatherMonitorBehaviour.WeatherSystemGameObject = weatherSystemGameObject;

            return weatherMonitorBehaviour;
        }
    }
}