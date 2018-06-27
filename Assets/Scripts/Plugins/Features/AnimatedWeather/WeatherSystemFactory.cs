using Assets.Scripts.Unity.Resources;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public sealed class WeatherSystemFactory : IWeatherSystemFactory
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly IWeatherMonitorBehaviourStitcher _weatherMonitorBehaviourStitcher;

        public WeatherSystemFactory(
            IPrefabCreator prefabCreator,
            IWeatherMonitorBehaviourStitcher weatherMonitorBehaviourStitcher)
        {
            _prefabCreator = prefabCreator;
            _weatherMonitorBehaviourStitcher = weatherMonitorBehaviourStitcher;
        }

        public GameObject Create()
        {
            var weatherSystem = _prefabCreator.Create<GameObject>("Weather/Prefabs/WeatherSystem");
            _weatherMonitorBehaviourStitcher.Attach(weatherSystem);
            return weatherSystem;
        }
    }
}