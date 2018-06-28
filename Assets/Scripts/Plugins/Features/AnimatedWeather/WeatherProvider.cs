using System;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Weather;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public sealed class WeatherProvider : IWeatherProvider
    {
        private readonly IReadOnlyWeatherManager _weatherManager;

        private IWeather _currentWeather;
        private IIdentifier _currentWeatherId;

        public WeatherProvider(IReadOnlyWeatherManager weatherManager)
        {
            _weatherManager = weatherManager;
        }

        public IWeather GetWeather()
        {
            var nextWeatherId = _weatherManager.WeatherId;
            if (Equals(_currentWeatherId, nextWeatherId))
            {
                return _currentWeather;
            }

            _currentWeather = new Weather()
            {
                //
                // TODO: load this from the weather info on the manager
                //
                Duration = TimeSpan.FromSeconds(5),
                TransitionInDuration = TimeSpan.FromSeconds(5),
                TransitionOutDuration = TimeSpan.FromSeconds(5),
                //
                // TODO: should there be a translation here?
                //
                WeatherResourceId = nextWeatherId,
            };
            _currentWeatherId = nextWeatherId;
            return _currentWeather;
        }
    }
}