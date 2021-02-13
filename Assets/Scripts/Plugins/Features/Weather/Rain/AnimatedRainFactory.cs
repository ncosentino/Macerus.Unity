using Assets.Scripts.Plugins.Features.AnimatedWeather.Api;
using Assets.Scripts.Unity.Resources.Prefabs;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedRain
{
    public sealed class AnimatedRainFactory : IDiscoverableAnimatedWeatherPlugin
    {
        private readonly IPrefabCreator _prefabCreator;

        public AnimatedRainFactory(IPrefabCreator prefabCreator)
        {
            _prefabCreator = prefabCreator;
        }

        public IIdentifier WeatherResourceId { get; } = new StringIdentifier("rain");

        public GameObject Create()
        {
            var weatherGameObject = _prefabCreator.Create<GameObject>("Weather/Prefabs/AnimatedRain");
            return weatherGameObject;
        }
    }
}