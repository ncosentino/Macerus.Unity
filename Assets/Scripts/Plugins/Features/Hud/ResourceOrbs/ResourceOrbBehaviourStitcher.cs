using Assets.Scripts.Unity;

using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Assets.Scripts.Plugins.Features.Hud.ResourceOrbs
{
    public sealed class ResourceOrbBehaviourStitcher : IResourceOrbBehaviourStitcher
    {
        private readonly ITimeProvider _timeProvider;
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;

        public ResourceOrbBehaviourStitcher(
            ITimeProvider timeProvider,
            IReadOnlyMapGameObjectManager mapGameObjectManager,
            IStatCalculationServiceAmenity statCalculationServiceAmenity)
        {
            _timeProvider = timeProvider;
            _mapGameObjectManager = mapGameObjectManager;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
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
            resourceOrbBehaviour.StatCalculationServiceAmenity = _statCalculationServiceAmenity;
            resourceOrbBehaviour.CurrentStatDefinitionId = currentStatDefinitionId;
            resourceOrbBehaviour.MaximumStatDefinitionId = maximumStatDefinitionId;
            resourceOrbBehaviour.ResourceOrbPrefab = resourceOrbPrefab;

            return resourceOrbBehaviour;
        }
    }
}
