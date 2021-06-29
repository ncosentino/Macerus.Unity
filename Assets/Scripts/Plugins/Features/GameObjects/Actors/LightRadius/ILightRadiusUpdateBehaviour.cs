using Assets.Scripts.Unity;

using Macerus.Plugins.Features.GameObjects.Actors.Default.LightRadius;
using Macerus.Plugins.Features.Stats.Api;

using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public interface ILightRadiusUpdateBehaviour : IReadOnlyLightRadiusUpdateBehaviour
    {
        new IGameObject GameObject { get; set; }
        
        new ILightRadiusPrefab LightRadiusPrefab { get; set; }

        new IStatCalculationServiceAmenity StatCalculationServiceAmenity { get; set; }

        new ILightRadiusIdentifiers LightRadiusIdentifiers { get; set; }

        new ITimeProvider TimeProvider { get; set; }
    }
}