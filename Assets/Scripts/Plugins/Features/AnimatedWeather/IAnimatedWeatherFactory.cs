using ProjectXyz.Api.Framework;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public interface IAnimatedWeatherFactory
    {
        GameObject Create(IIdentifier weatherResourceId);
    }
}