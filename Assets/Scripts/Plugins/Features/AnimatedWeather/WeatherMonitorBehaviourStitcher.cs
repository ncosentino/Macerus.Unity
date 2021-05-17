using System;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class WeatherMonitorBehaviourStitcher : IWeatherMonitorBehaviourStitcher
    {
        private readonly IAnimatedWeatherFactory _animatedWeatherFactory;
        private readonly ILogger _logger;
        private readonly IWeatherProvider _weatherProvider;
        private readonly IFadeAndKillBehaviourStitcher _fadeAndKillBehaviourStitcher;
        private readonly IFadeInBehaviourStitcher _fadeInBehaviourStitcher;

        public WeatherMonitorBehaviourStitcher(
            IWeatherProvider weatherProvider,
            IFadeAndKillBehaviourStitcher fadeAndKillBehaviourStitcher,
            IFadeInBehaviourStitcher fadeInBehaviourStitcher,
            IAnimatedWeatherFactory animatedWeatherFactory,
            ILogger logger)
        {
            _weatherProvider = weatherProvider;
            _fadeAndKillBehaviourStitcher = fadeAndKillBehaviourStitcher;
            _fadeInBehaviourStitcher = fadeInBehaviourStitcher;
            _animatedWeatherFactory = animatedWeatherFactory;
            _logger = logger;
        }

        public void Attach(GameObject weatherSystemGameObject)
        {
            var weatherMonitorBehaviour = weatherSystemGameObject.AddComponent<WeatherMonitorBehaviour>();
            weatherMonitorBehaviour.FadeInBehaviourStitcher = _fadeInBehaviourStitcher;
            weatherMonitorBehaviour.FadeAndKillBehaviourStitcher = _fadeAndKillBehaviourStitcher;
            weatherMonitorBehaviour.WeatherProvider = _weatherProvider;
            weatherMonitorBehaviour.Logger = _logger;
            weatherMonitorBehaviour.AnimatedWeatherFactory = _animatedWeatherFactory;

            weatherMonitorBehaviour.UpdateDelay = TimeSpan.FromSeconds(0.25);
            weatherMonitorBehaviour.WeatherSystemGameObject = weatherSystemGameObject;
        }
    }
}