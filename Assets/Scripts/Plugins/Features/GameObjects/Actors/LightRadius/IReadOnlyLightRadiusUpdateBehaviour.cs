using Assets.Scripts.Unity;

using Macerus.Plugins.Features.GameObjects.Actors.Default.LightRadius;
using Macerus.Plugins.Features.Stats.Api;

using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public interface IReadOnlyLightRadiusUpdateBehaviour
    {
        IGameObject GameObject { get; }
        
        ILightRadiusPrefab LightRadiusPrefab { get; }

        IStatCalculationServiceAmenity StatCalculationServiceAmenity { get; }

        ILightRadiusIdentifiers LightRadiusIdentifiers { get; }

        ITimeProvider TimeProvider { get; }
    }
}