using Assets.Scripts.Unity;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public interface ILightRadiusUpdateBehaviour : IReadOnlyLightRadiusUpdateBehaviour
    {
        new IGameObject GameObject { get; set; }
        new ILightRadiusPrefab LightRadiusPrefab { get; set; }
        new IStatCalculationService StatCalculationService { get; set; }
        new IReadOnlyStatDefinitionToTermMappingRepositoryFacade StatDefinitionToTermMappingRepository { get; set; }
        new ITimeProvider TimeProvider { get; set; }
    }
}