using System;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather.Api
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public interface IWeatherMonitorBehaviour : IReadOnlyWeatherMonitorBehaviour
    {
        new IWeatherProvider WeatherProvider { get; set; }

        new TimeSpan UpdateDelay { get; set; }

        new GameObject WeatherSystemGameObject { get; set; }

        new IFadeAndKillBehaviourStitcher FadeAndKillBehaviourStitcher { get; set; }

        new IFadeInBehaviourStitcher FadeInBehaviourStitcher { get; set; }

        new ILogger Logger { get; set; }

        new IAnimatedWeatherFactory AnimatedWeatherFactory { get; set; }
    }
}