﻿using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface IMapBehaviour : IReadonlyMapBehaviour
    {
        new IReadOnlyMapGameObjectManager MapGameObjectManager { get; set; }

        new IMapProvider MapProvider { get; set; }

        new IMapFormatter MapFormatter { get; set; }

        new IWeatherManager WeatherManager { get; set; }

        new IWeatherTableRepositoryFacade WeatherTableRepositoryFacade { get; set; }

        new IFilterContextFactory FilterContextFactory { get; set; }
    }
}