using System;
using ProjectXyz.Api.Framework;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public interface IAnimatedWeatherFactoryRegistrar
    {
        void Register(IIdentifier weatherResourceId, Func<GameObject> callback);
    }
}