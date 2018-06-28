using System;
using Assets.Scripts.Plugins.Features.AnimatedWeather.Api;
using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Framework.Contracts;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class WeatherMonitorBehaviour :
        MonoBehaviour,
        IWeatherMonitorBehaviour
    {
        public IWeatherProvider WeatherProvider { get; set; }

        public TimeSpan UpdateDelay { get; set; }

        public GameObject WeatherSystemGameObject { get; set; }

        public IObjectDestroyer ObjectDestroyer { get; set; }

        public ILogger Logger { get; set; }

        public IAnimatedWeatherFactory AnimatedWeatherFactory { get; set; }

        private float _triggerTime;
        private IWeather _currentWeather;

        private void Start()
        {
            Contract.RequiresNotNull(
                WeatherProvider,
                $"{nameof(WeatherProvider)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                WeatherSystemGameObject,
                $"{nameof(WeatherSystemGameObject)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                ObjectDestroyer,
                $"{nameof(ObjectDestroyer)} was not set on '{gameObject}.{this}'.");
            Contract.Requires(
                UpdateDelay >= TimeSpan.Zero,
                $"{nameof(UpdateDelay)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                Logger,
                $"{nameof(Logger)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                AnimatedWeatherFactory,
                $"{nameof(AnimatedWeatherFactory)} was not set on '{gameObject}.{this}'.");
            ResetTriggerTime();
        }

        private void FixedUpdate()
        {
            if (Time.fixedTime < _triggerTime)
            {
                return;
            }

            ResetTriggerTime();

            var nextWeather = WeatherProvider.GetWeather();
            if (_currentWeather != nextWeather)
            {
                Logger.Debug($"Switching weather from '{_currentWeather}' to '{nextWeather}'...");

                //
                // TODO: transition the weather smoothly instead of immediately
                //

                // remove existing weather
                foreach (var child in WeatherSystemGameObject.GetChildGameObjects())
                {
                    ObjectDestroyer.Destroy(child);
                }

                // create the new weather and add it to our object
                var animatedWeatherObject = AnimatedWeatherFactory.Create(nextWeather.WeatherResourceId);
                animatedWeatherObject.transform.SetParent(
                    gameObject.transform,
                    false);

                _currentWeather = nextWeather;
                Logger.Debug($"Switched weather from '{_currentWeather}' to '{nextWeather}'.");
            }
        }

        private void ResetTriggerTime()
        {
            _triggerTime = Time.fixedTime + (float)UpdateDelay.TotalSeconds;
        }
    }
}