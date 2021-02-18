using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Resources.Prefabs;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public sealed class LightRadiusStitcher : ILightRadiusStitcher
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly IStatCalculationService _statCalculationService;
        private readonly IReadOnlyStatDefinitionToTermMappingRepositoryFacade _statDefinitionToTermMappingRepository;
        private readonly ITimeProvider _timeProvider;

        public LightRadiusStitcher(
            IPrefabCreator prefabCreator,
            IStatCalculationService statCalculationService,
            IReadOnlyStatDefinitionToTermMappingRepositoryFacade statDefinitionToTermMappingRepository,
            ITimeProvider timeProvider)
        {
            _prefabCreator = prefabCreator;
            _statCalculationService = statCalculationService;
            _statDefinitionToTermMappingRepository = statDefinitionToTermMappingRepository;
            _timeProvider = timeProvider;
        }

        public ILightRadiusPrefab Stitch(IGameObject gameObject, GameObject unityGameObject)
        {
            var lightRadiusObject = _prefabCreator.Create<GameObject>("Mapping/Prefabs/LightRadius");
            lightRadiusObject.transform.position = new Vector3(0, 0, -1);
            lightRadiusObject.transform.SetParent(unityGameObject.transform, false);

            var lightRadiusPrefab = new LightRadiusPrefab(lightRadiusObject);
            var lightRadiusUpdateBehaviour = lightRadiusObject.AddComponent<LightRadiusUpdateBehaviour>();
            lightRadiusUpdateBehaviour.StatCalculationService = _statCalculationService;
            lightRadiusUpdateBehaviour.StatDefinitionToTermMappingRepository = _statDefinitionToTermMappingRepository;
            lightRadiusUpdateBehaviour.TimeProvider = _timeProvider;
            lightRadiusUpdateBehaviour.LightRadiusPrefab = lightRadiusPrefab;
            lightRadiusUpdateBehaviour.GameObject = gameObject;

            return lightRadiusPrefab;
        }
    }
}