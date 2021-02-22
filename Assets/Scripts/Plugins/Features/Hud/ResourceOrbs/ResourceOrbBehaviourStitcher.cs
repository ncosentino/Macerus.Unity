
using Assets.Scripts.Unity;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace Assets.Scripts.Plugins.Features.Hud.ResourceOrbs
{
    public sealed class ResourceOrbBehaviourStitcher : IResourceOrbBehaviourStitcher
    {
        private readonly ITimeProvider _timeProvider;
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IStatCalculationService _statCalculationService;

        public ResourceOrbBehaviourStitcher(
            ITimeProvider timeProvider,
            IGameObjectManager gameObjectManager,
            IStatCalculationService statCalculationService)
        {
            _timeProvider = timeProvider;
            _gameObjectManager = gameObjectManager;
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
            resourceOrbBehaviour.GameObjectManager = _gameObjectManager;
            resourceOrbBehaviour.StatCalculationService = _statCalculationService;
            resourceOrbBehaviour.CurrentStatDefinitionId = currentStatDefinitionId;
            resourceOrbBehaviour.MaximumStatDefinitionId = maximumStatDefinitionId;
            resourceOrbBehaviour.ResourceOrbPrefab = resourceOrbPrefab;

            return resourceOrbBehaviour;
        }
    }
}
