using System;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather.Api
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public interface IReadOnlyWeatherMonitorBehaviour
    {
        IWeatherProvider WeatherProvider { get; }

        TimeSpan UpdateDelay { get; }

        GameObject WeatherSystemGameObject { get; }

        IFadeAndKillBehaviourStitcher FadeAndKillBehaviourStitcher { get; }

        IFadeInBehaviourStitcher FadeInBehaviourStitcher { get; }

        ILogger Logger { get; }

        IAnimatedWeatherFactory AnimatedWeatherFactory { get; }
    }
}