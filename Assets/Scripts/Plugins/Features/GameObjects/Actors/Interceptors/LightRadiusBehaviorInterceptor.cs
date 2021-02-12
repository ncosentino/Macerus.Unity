using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Unity.Resources;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Enchantments.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public sealed class LightRadiusBehaviorInterceptor : IDiscoverableGameObjectBehaviorInterceptor
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly IStatCalculationService _statCalculationService;
        private readonly IReadOnlyStatDefinitionToTermMappingRepositoryFacade _statDefinitionToTermMappingRepository;

        public LightRadiusBehaviorInterceptor(
            IPrefabCreator prefabCreator,
            IStatCalculationService statCalculationService,
            IReadOnlyStatDefinitionToTermMappingRepositoryFacade statDefinitionToTermMappingRepository)
        {
            _prefabCreator = prefabCreator;
            _statCalculationService = statCalculationService;
            _statDefinitionToTermMappingRepository = statDefinitionToTermMappingRepository;
        }

        public void Intercept(
            IGameObject gameObject, 
            GameObject unityGameObject)
        {
            if (!gameObject.Has<IHasStatsBehavior>())
            {
                return;
            }

            var context = new StatCalculationContext(
                new IComponent[0],
                new IEnchantment[0]);
            var lightRadiusRadius = _statCalculationService.GetStatValue(
                gameObject,
                _statDefinitionToTermMappingRepository
                    .GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_RADIUS")
                    .StatDefinitionId,
                context);
            var lightRadiusIntensity = _statCalculationService.GetStatValue(
                gameObject,
                 _statDefinitionToTermMappingRepository
                    .GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_INTENSITY")
                    .StatDefinitionId,
                context);
            var lightRadiusRed = _statCalculationService.GetStatValue(
                gameObject,
                _statDefinitionToTermMappingRepository
                    .GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_RED")
                    .StatDefinitionId,
                context);
            var lightRadiusGreen = _statCalculationService.GetStatValue(
                gameObject,
                _statDefinitionToTermMappingRepository
                    .GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_GREEN")
                    .StatDefinitionId,
                context);
            var lightRadiusBlue = _statCalculationService.GetStatValue(
                gameObject,
                _statDefinitionToTermMappingRepository
                    .GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_BLUE")
                    .StatDefinitionId,
                context);

            var lightRadiusObject = _prefabCreator.Create<GameObject>("Mapping/Prefabs/LightRadius");
            lightRadiusObject.transform.position = new Vector3(0, 0, -1);
            lightRadiusObject.transform.SetParent(unityGameObject.transform, false);

            var lightComponent = lightRadiusObject.GetComponent<Light>();
            lightComponent.color = new Color(
                (float)lightRadiusRed,
                (float)lightRadiusGreen,
                (float)lightRadiusBlue);
            lightComponent.range = (float)lightRadiusRadius;
            lightComponent.intensity = (float)lightRadiusIntensity;
        }
    }
}