using System;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Weather;
using ProjectXyz.Shared.Framework;

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
                MinOpacity = 0,
                //
                // TODO: should there be a translation here?
                //
                WeatherResourceId = nextWeatherId,
                //
                // FIXME: just a total hack for testing until values are loaded from backend
                //
                MaxOpacity = Equals(nextWeatherId, new StringIdentifier("clear")) ? 0 : 1,
            };
            _currentWeatherId = nextWeatherId;
            return _currentWeather;
        }
    }
}