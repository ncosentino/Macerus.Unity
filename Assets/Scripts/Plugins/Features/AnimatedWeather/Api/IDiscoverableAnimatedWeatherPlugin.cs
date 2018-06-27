using ProjectXyz.Api.Framework;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather.Api
{
    public interface IDiscoverableAnimatedWeatherPlugin
    {
        IIdentifier WeatherResourceId { get; }

        GameObject Create();
    }
}