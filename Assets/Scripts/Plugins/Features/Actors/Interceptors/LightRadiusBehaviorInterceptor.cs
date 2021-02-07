﻿using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Scenes.Explore.GameObjects;
using Assets.Scripts.Unity.Resources;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Enchantments.Stats;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.Interceptors
{
    public sealed class LightRadiusBehaviorInterceptor : IGameObjectBehaviorInterceptor
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly IStatCalculationService _statCalculationService;
        private readonly IReadOnlyCollection<IStatDefinitionToTermMappingRepository> _statDefinitionToTermMappingRepositories;

        public LightRadiusBehaviorInterceptor(
            IPrefabCreator prefabCreator,
            IStatCalculationService statCalculationService,
            IEnumerable<IStatDefinitionToTermMappingRepository> statDefinitionToTermMappingRepositories)
        {
            _prefabCreator = prefabCreator;
            _statCalculationService = statCalculationService;
            _statDefinitionToTermMappingRepositories = statDefinitionToTermMappingRepositories.ToArray();
        }

        public void Intercept(
            IGameObject gameObject, 
            GameObject unityGameObject)
        {
            var context = new StatCalculationContext(
                new IComponent[0],
                new IEnchantment[0]);
            var lightRadiusRadius = _statCalculationService.GetStatValue(
                gameObject,
                _statDefinitionToTermMappingRepositories
                    .Select(x => x.GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_RADIUS"))
                    .Single()
                    .StatDefinitionId,
                context);
            var lightRadiusIntensity = _statCalculationService.GetStatValue(
                gameObject,
                _statDefinitionToTermMappingRepositories
                    .Select(x => x.GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_INTENSITY"))
                    .Single()
                    .StatDefinitionId,
                context);
            var lightRadiusRed = _statCalculationService.GetStatValue(
                gameObject,
                _statDefinitionToTermMappingRepositories
                    .Select(x => x.GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_RED"))
                    .Single()
                    .StatDefinitionId,
                context);
            var lightRadiusGreen = _statCalculationService.GetStatValue(
                gameObject,
                _statDefinitionToTermMappingRepositories
                    .Select(x => x.GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_GREEN"))
                    .Single()
                    .StatDefinitionId,
                context);
            var lightRadiusBlue = _statCalculationService.GetStatValue(
                gameObject,
                _statDefinitionToTermMappingRepositories
                    .Select(x => x.GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_BLUE"))
                    .Single()
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