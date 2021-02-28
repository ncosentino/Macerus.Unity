using System;
using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public sealed class AnimatedWeather : IAnimatedWeather
    {
        public IIdentifier WeatherResourceId { get; set; }

        public TimeSpan Duration { get; set; }

        public TimeSpan TransitionInDuration { get; set; }

        public TimeSpan TransitionOutDuration { get; set; }

        public double MinOpacity { get; set; }

        public double MaxOpacity { get; set; }
    }
}