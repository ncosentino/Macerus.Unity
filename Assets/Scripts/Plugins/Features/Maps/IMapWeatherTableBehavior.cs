
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public interface IMapWeatherTableBehavior : IBehavior
    {
        IIdentifier WeatherTableId { get; }
    }
}