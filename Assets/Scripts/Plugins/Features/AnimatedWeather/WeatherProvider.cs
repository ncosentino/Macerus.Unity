using System;

using Assets.Scripts.Api.GameObjects;

using Macerus.Plugins.Content.Weather;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public sealed class WeatherProvider : IWeatherProvider
    {
        private readonly IReadOnlyWeatherManager _weatherManager;

        private IWeather _currentWeather;
        private IAnimatedWeather _currentAnimatedWeather;

        public WeatherProvider(IReadOnlyWeatherManager weatherManager)
        {
            _weatherManager = weatherManager;
        }

        public IAnimatedWeather GetWeather()
        {
            var nextWeather = _weatherManager.Weather;
            if (Equals(_currentWeather, nextWeather))
            {
                return _currentAnimatedWeather;
            }

            var weatherId = nextWeather
                .GetOnly<IIdentifierBehavior>()
                .Id;
            _currentAnimatedWeather = new AnimatedWeather()
            {
                Duration = IntervalToTimespan(nextWeather.Duration),
                TransitionInDuration = IntervalToTimespan(nextWeather.TransitionInDuration),
                TransitionOutDuration = IntervalToTimespan(nextWeather.TransitionOutDuration),
                WeatherResourceId = nextWeather.GetOnly<IPrefabResourceBehavior>().PrefabResourceId,
                //
                // FIXME: just a total hack for testing until values are loaded from backend
                //
                MinOpacity = 0,
                MaxOpacity = Equals(weatherId, WeatherIds.Clear) ? 0 : 1,
            };
            _currentWeather = nextWeather;
            return _currentAnimatedWeather;
        }

        private TimeSpan IntervalToTimespan(IInterval interval)
        {
            // FIXME: we need to build some better interval-to-timespan conversions
            var milliseconds = ((IInterval<double>)interval).Value;
            var timespan = TimeSpan.FromMilliseconds(milliseconds);
            return timespan;
        }
    }
}