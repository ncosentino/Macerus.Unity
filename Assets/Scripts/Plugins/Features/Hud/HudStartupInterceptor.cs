using System.Linq;

using Assets.Scripts.Plugins.Features.Hud.ResourceOrbs;
using Assets.Scripts.Scenes.Explore.Api;
using Assets.Scripts.Unity.GameObjects;

using NexusLabs.Contracts;

using ProjectXyz.Api.Stats;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud
{
    public sealed class HudStartupInterceptor : IExploreSceneStartupInterceptor
    {
        private readonly IResourceOrbBehaviourStitcher _resourceOrbBehaviourStitcher;
        private readonly IReadOnlyStatDefinitionToTermMappingRepository _statDefinitionToTermMappingRepository;

        public HudStartupInterceptor(
            IResourceOrbBehaviourStitcher resourceOrbBehaviourStitcher,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            _resourceOrbBehaviourStitcher = resourceOrbBehaviourStitcher;
            _statDefinitionToTermMappingRepository = statDefinitionToTermMappingRepository;
        }

        public void Intercept(GameObject explore)
        {
            SetupResourceOrbs(explore);
        }

        private void SetupResourceOrbs(GameObject explore)
        {
            var healthOrbObject = explore
                .GetChildGameObjects(false)
                .FirstOrDefault(x => x.name == "HealthOrb");
            Contract.RequiresNotNull(
                healthOrbObject,
                $"Could not find health orb within children of '{explore}'.");

            var manaOrbObject = explore
                .GetChildGameObjects(false)
                .FirstOrDefault(x => x.name == "ManaOrb");
            Contract.RequiresNotNull(
                healthOrbObject,
                $"Could not find mana orb within children of '{explore}'.");

            var lifeCurrentStatId = _statDefinitionToTermMappingRepository
                .GetStatDefinitionToTermMappingByTerm("LIFE_CURRENT")
                .StatDefinitionId;
            var lifeMaximumStatId = _statDefinitionToTermMappingRepository
                .GetStatDefinitionToTermMappingByTerm("LIFE_MAXIMUM")
                .StatDefinitionId;
            _resourceOrbBehaviourStitcher.Stitch(
                new ResourceOrbPrefab(healthOrbObject),
                lifeCurrentStatId,
                lifeMaximumStatId);

            var manaCurrentStatId = _statDefinitionToTermMappingRepository
                .GetStatDefinitionToTermMappingByTerm("MANA_CURRENT")
                .StatDefinitionId;
            var manaMaximumStatId = _statDefinitionToTermMappingRepository
                .GetStatDefinitionToTermMappingByTerm("MANA_MAXIMUM")
                .StatDefinitionId;
            _resourceOrbBehaviourStitcher.Stitch(
                new ResourceOrbPrefab(manaOrbObject),
                manaCurrentStatId,
                manaMaximumStatId);
        }
    }
}
