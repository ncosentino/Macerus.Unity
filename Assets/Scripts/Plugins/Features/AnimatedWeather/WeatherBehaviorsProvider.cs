using System.Collections.Generic;

using Assets.Scripts.Shared.GameObjects;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public sealed class WeatherBehaviorsProvider : IDiscoverableWeatherBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors)
        {
            // FIXME: should there be a translation here?
            var weatherId = baseBehaviors
                .GetOnly<IIdentifierBehavior>()
                .Id;
            yield return new HasPrefabResourceBehavior()
            {
                PrefabResourceId = weatherId,
            };
        }
    }
}