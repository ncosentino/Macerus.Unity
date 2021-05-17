using System;
using System.Linq;
using Assets.Scripts.Unity.GameObjects;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class WeatherMonitorBehaviour : MonoBehaviour
    {
        public IWeatherProvider WeatherProvider { get; set; }

        public TimeSpan UpdateDelay { get; set; }

        public GameObject WeatherSystemGameObject { get; set; }

        public IFadeAndKillBehaviourStitcher FadeAndKillBehaviourStitcher { get; set; }

        public IFadeInBehaviourStitcher FadeInBehaviourStitcher { get; set; }

        public ILogger Logger { get; set; }

        public IAnimatedWeatherFactory AnimatedWeatherFactory { get; set; }

        private float _triggerTime;
        private IAnimatedWeather _currentWeather;

        private void Start()
        {
            Contract.RequiresNotNull(
                WeatherProvider,
                $"{nameof(WeatherProvider)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                WeatherSystemGameObject,
                $"{nameof(WeatherSystemGameObject)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                FadeAndKillBehaviourStitcher,
                $"{nameof(FadeAndKillBehaviourStitcher)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                FadeInBehaviourStitcher,
                $"{nameof(FadeInBehaviourStitcher)} was not set on '{gameObject}.{this}'.");
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
                Logger.Debug($"Switching weather from '{_currentWeather?.WeatherResourceId}' to '{nextWeather?.WeatherResourceId}'...");

                // transition all non-transitioning weather out
                foreach (var child in WeatherSystemGameObject
                    .GetChildGameObjects()
                    .Where(x => 
                        x.GetComponent<FadeAndKillBehaviour>() == null &&
                        x.GetComponent<FadeInBehaviour>() == null))
                {
                    FadeAndKillBehaviourStitcher.Attach(
                        child,
                        _currentWeather.TransitionOutDuration,
                        _currentWeather.MaxOpacity,
                        _currentWeather.MinOpacity);
                }

                // create the new weather and add it to our object
                var animatedWeatherObject = AnimatedWeatherFactory.Create(nextWeather.WeatherResourceId);
                animatedWeatherObject.transform.SetParent(
                    WeatherSystemGameObject.transform,
                    false);
                FadeInBehaviourStitcher.Attach(
                    animatedWeatherObject,
                    nextWeather.TransitionOutDuration,
                    nextWeather.MinOpacity,
                    nextWeather.MaxOpacity);

                Logger.Debug($"Switched weather from '{_currentWeather?.WeatherResourceId}' to '{nextWeather?.WeatherResourceId}'.");
                _currentWeather = nextWeather;
            }
        }

        private void ResetTriggerTime()
        {
            _triggerTime = Time.fixedTime + (float)UpdateDelay.TotalSeconds;
        }
    }
}