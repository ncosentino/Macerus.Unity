using System;
using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public sealed class Weather : IWeather
    {
        public IIdentifier WeatherResourceId { get; set; }

        public TimeSpan Duration { get; set; }

        public TimeSpan TransitionInDuration { get; set; }

        public TimeSpan TransitionOutDuration { get; set; }
    }
}