using System;
using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Plugins.Features.Weather;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather.Api
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public interface IReadOnlyWeatherMonitorBehaviour
    {
        IReadOnlyWeatherManager WeatherManager { get; }

        TimeSpan UpdateDelay { get; }

        GameObject WeatherSystemGameObject { get; }

        IObjectDestroyer ObjectDestroyer { get; }

        ILogger Logger { get; }

        IAnimatedWeatherFactory AnimatedWeatherFactory { get; }
    }
}