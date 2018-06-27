using Assets.Scripts.Plugins.Features.AnimatedWeather.Api;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public interface IWeatherMonitorBehaviourStitcher
    {
        IReadOnlyWeatherMonitorBehaviour Attach(GameObject weatherSystemGameObject);
    }
}