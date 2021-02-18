using Assets.Scripts.Unity;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public interface IReadOnlyLightRadiusUpdateBehaviour
    {
        IGameObject GameObject { get; }
        ILightRadiusPrefab LightRadiusPrefab { get; }
        IStatCalculationService StatCalculationService { get; }
        IReadOnlyStatDefinitionToTermMappingRepositoryFacade StatDefinitionToTermMappingRepository { get; }
        ITimeProvider TimeProvider { get; }
    }
}