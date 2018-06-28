using System;
using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public interface IWeather : IReadOnlyWeather
    {
        new IIdentifier WeatherResourceId { get; set; }

        new TimeSpan Duration { get; set; }

        new TimeSpan TransitionInDuration { get; set; }

        new TimeSpan TransitionOutDuration { get; set; }
    }
}