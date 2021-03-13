using Assets.Scripts.Unity;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Assets.Scripts.Plugins.Features.Hud.ResourceOrbs
{
    public sealed class ResourceOrbBehaviourStitcher : IResourceOrbBehaviourStitcher
    {
        private readonly ITimeProvider _timeProvider;
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;
        private readonly IStatCalculationService _statCalculationService;

        public ResourceOrbBehaviourStitcher(
            ITimeProvider timeProvider,
            IReadOnlyMapGameObjectManager mapGameObjectManager,
            IStatCalculationService statCalculationService)
        {
            _timeProvider = timeProvider;
            _mapGameObjectManager = mapGameObjectManager;
            _statCalculationService = statCalculationService;
        }

        public IReadOnlyResourceOrbBehaviour Stitch(
            IResourceOrbPrefab resourceOrbPrefab,
            IIdentifier currentStatDefinitionId,
            IIdentifier maximumStatDefinitionId)
        {
            var resourceOrbBehaviour = resourceOrbPrefab
                .GameObject
                .AddComponent<ResourceOrbBehaviour>();
            resourceOrbBehaviour.TimeProvider = _timeProvider;
            resourceOrbBehaviour.MapGameObjectManager = _mapGameObjectManager;
            resourceOrbBehaviour.StatCalculationService = _statCalculationService;
            resourceOrbBehaviour.CurrentStatDefinitionId = currentStatDefinitionId;
            resourceOrbBehaviour.MaximumStatDefinitionId = maximumStatDefinitionId;
            resourceOrbBehaviour.ResourceOrbPrefab = resourceOrbPrefab;

            return resourceOrbBehaviour;
        }
    }
}
