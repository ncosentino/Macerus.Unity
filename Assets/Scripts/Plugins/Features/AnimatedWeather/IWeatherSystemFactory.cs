using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public interface IWeatherSystemFactory
    {
        GameObject Create();
    }
}