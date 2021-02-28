using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public sealed class MapWeatherTableBehavior :
        BaseBehavior,
        IMapWeatherTableBehavior
    {
        public MapWeatherTableBehavior(IIdentifier weatherTableId)
        {
            WeatherTableId = weatherTableId;
        }

        public IIdentifier WeatherTableId { get; }
    }
}