using Assets.Scripts.Plugins.Features.AnimatedWeather.Api;
using Assets.Scripts.Unity.Resources;
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Weather.Clear
{
    public sealed class ClearWeatherFactory : IDiscoverableAnimatedWeatherPlugin
    {
        private readonly IPrefabCreator _prefabCreator;

        public ClearWeatherFactory(IPrefabCreator prefabCreator)
        {
            _prefabCreator = prefabCreator;
        }

        public IIdentifier WeatherResourceId { get; } = new StringIdentifier("clear");

        public GameObject Create()
        {
            var weatherGameObject = _prefabCreator.Create<GameObject>("Weather/Prefabs/Clear");
            return weatherGameObject;
        }
    }
}