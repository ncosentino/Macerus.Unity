using Assets.Scripts.Unity;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Enchantments.Stats;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public sealed class LightRadiusUpdateBehaviour :
        MonoBehaviour,
        ILightRadiusUpdateBehaviour
    {
        private double _lastUpdate;

        public ITimeProvider TimeProvider { get; set; }

        public IGameObject GameObject { get; set; }

        public ILightRadiusPrefab LightRadiusPrefab { get; set; }

        public IStatCalculationService StatCalculationService { get; set; }

        public IReadOnlyStatDefinitionToTermMappingRepositoryFacade StatDefinitionToTermMappingRepository { get; set; }

        private void FixedUpdate()
        {
            var secondsSinceLastUpdate = TimeProvider.SecondsSinceStartOfGame - _lastUpdate;
            if (secondsSinceLastUpdate < 1)
            {
                return;
            }

            var context = new StatCalculationContext(
                new IComponent[0],
                new IEnchantment[0]);
            var lightRadiusRadius = StatCalculationService.GetStatValue(
                GameObject,
                StatDefinitionToTermMappingRepository
                    .GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_RADIUS")
                    .StatDefinitionId,
                context);
            var lightRadiusIntensity = StatCalculationService.GetStatValue(
                GameObject,
                 StatDefinitionToTermMappingRepository
                    .GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_INTENSITY")
                    .StatDefinitionId,
                context);
            var lightRadiusRed = StatCalculationService.GetStatValue(
                GameObject,
                StatDefinitionToTermMappingRepository
                    .GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_RED")
                    .StatDefinitionId,
                context);
            var lightRadiusGreen = StatCalculationService.GetStatValue(
                GameObject,
                StatDefinitionToTermMappingRepository
                    .GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_GREEN")
                    .StatDefinitionId,
                context);
            var lightRadiusBlue = StatCalculationService.GetStatValue(
                GameObject,
                StatDefinitionToTermMappingRepository
                    .GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_BLUE")
                    .StatDefinitionId,
                context);

            LightRadiusPrefab.Light.color = new Color(
                (float)lightRadiusRed,
                (float)lightRadiusGreen,
                (float)lightRadiusBlue);
            LightRadiusPrefab.Light.range = (float)lightRadiusRadius;
            LightRadiusPrefab.Light.intensity = (float)lightRadiusIntensity;

            _lastUpdate = TimeProvider.SecondsSinceStartOfGame;
        }
    }
}