using System;
using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public interface IReadOnlyWeather
    {
        IIdentifier WeatherResourceId { get; }

        TimeSpan Duration { get; }

        TimeSpan TransitionInDuration { get; }

        TimeSpan TransitionOutDuration { get; }
    }
}