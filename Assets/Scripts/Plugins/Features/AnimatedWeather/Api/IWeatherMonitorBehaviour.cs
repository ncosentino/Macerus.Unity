using System;
using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Plugins.Features.Weather;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather.Api
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public interface IWeatherMonitorBehaviour : IReadOnlyWeatherMonitorBehaviour
    {
        new IReadOnlyWeatherManager WeatherManager { get; set; }

        new TimeSpan UpdateDelay { get; set; }

        new GameObject WeatherSystemGameObject { get; set; }

        new IObjectDestroyer ObjectDestroyer { get; set; }

        new ILogger Logger { get; set; }

        new IAnimatedWeatherFactory AnimatedWeatherFactory { get; set; }
    }
}