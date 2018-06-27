using System;
using Assets.Scripts.Plugins.Features.AnimatedWeather.Api;
using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Contracts;
using ProjectXyz.Plugins.Features.Weather;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class WeatherMonitorBehaviour :
        MonoBehaviour,
        IWeatherMonitorBehaviour
    {
        public IReadOnlyWeatherManager WeatherManager { get; set; }

        public TimeSpan UpdateDelay { get; set; }

        public GameObject WeatherSystemGameObject { get; set; }

        public IObjectDestroyer ObjectDestroyer { get; set; }

        public ILogger Logger { get; set; }

        public IAnimatedWeatherFactory AnimatedWeatherFactory { get; set; }

        private float _triggerTime;
        private IIdentifier _currentWeatherId;

        private void Start()
        {
            Contract.RequiresNotNull(
                WeatherManager,
                $"{nameof(WeatherManager)} was not set on '{gameObject}.{this}'.");
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

            var nextWeatherId = WeatherManager.WeatherId;
            if (!Equals(_currentWeatherId, nextWeatherId))
            {
                Logger.Debug($"Switching weather from '{_currentWeatherId}' to '{nextWeatherId}'...");

                // TODO: transition the weather smoothly instead of immediately

                // remove existing weather
                foreach (var child in WeatherSystemGameObject.GetChildGameObjects())
                {
                    ObjectDestroyer.Destroy(child);
                }

                //
                // TODO: do we need to map a weather id to a resource id?
                //

                // create the new weather and add it to our object
                var animatedWeatherObject = AnimatedWeatherFactory.Create(nextWeatherId);
                animatedWeatherObject.transform.SetParent(
                    gameObject.transform,
                    false);

                _currentWeatherId = nextWeatherId;
                Logger.Debug($"Switched weather from '{_currentWeatherId}' to '{nextWeatherId}'.");
            }
        }

        private void ResetTriggerTime()
        {
            _triggerTime = Time.fixedTime + (float)UpdateDelay.TotalSeconds;
        }
    }
}