using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Shared.Framework;

namespace Assets.Scripts.Plugins.Features.Wip
{
    public sealed class WipModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<WipSkills>()
                .SingleInstance();
            builder
                .RegisterType<PlayerTestingBehaviorsInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }

    public sealed class PlayerTestingBehaviorsInterceptor : IDiscoverableActorBehaviorsInterceptor
    {
        private readonly IReadOnlyStatDefinitionToTermMappingRepository _statDefinitionToTermMappingRepository;
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly ISkillRepository _skillRepository;

        public PlayerTestingBehaviorsInterceptor(
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository,
            IFilterContextFactory filterContextFactory,
            ISkillRepository skillRepository)
        {
            _statDefinitionToTermMappingRepository = statDefinitionToTermMappingRepository;
            _filterContextFactory = filterContextFactory;
            _skillRepository = skillRepository;
        }

        public void Intercept(IReadOnlyCollection<IBehavior> behaviors)
        {
            if (!behaviors.Has<IPlayerControlledBehavior>())
            {
                return;
            }

            var mutableStats = behaviors.GetOnly<IHasMutableStatsBehavior>();
            mutableStats.MutateStats(stats =>
            {
                stats[_statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("LIFE_MAXIMUM").StatDefinitionId] = 100;
                stats[_statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("LIFE_CURRENT").StatDefinitionId] = 10;
                stats[_statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("MANA_MAXIMUM").StatDefinitionId] = 100;
                stats[_statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("MANA_CURRENT").StatDefinitionId] = 100;
            });

            var skillsBehavior = behaviors.GetOnly<IHasSkillsBehavior>();
            skillsBehavior.Add(new[]
            {
                _skillRepository
                    .GetSkills(_filterContextFactory.CreateFilterContextForSingle(new IFilterAttribute[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("id"),
                            new IdentifierFilterAttributeValue(new StringIdentifier("heal-self")),
                            true),
                    }))
                    .FirstOrDefault(),
        });
        }
    }

    public sealed class WipSkills
    {
        private readonly ISkillDefinitionRepositoryFacade _skillDefinitionRepositoryFacade;
        private readonly IStatCalculationService _statCalculationService;
        private readonly IStatCalculationContextFactory _statCalculationContextFactory;
        private readonly ILogger _logger;

        public WipSkills(
            ISkillDefinitionRepositoryFacade skillDefinitionRepositoryFacade,
            IStatCalculationService statCalculationService,
            IStatCalculationContextFactory statCalculationContextFactory,
            ILogger logger)
        {
            _skillDefinitionRepositoryFacade = skillDefinitionRepositoryFacade;
            _statCalculationService = statCalculationService;
            _statCalculationContextFactory = statCalculationContextFactory;
            _logger = logger;
        }

        public bool CanUseSkill(
            IGameObject actor,
            IGameObject skill)
        {
            if (skill.TryGetFirst<ISkillResourceUsageBehavior>(out var skillResourceUsageBehavior) &&
                skillResourceUsageBehavior.StaticStatRequirements.Any())
            {
                var statCalculationContext = _statCalculationContextFactory.Create(
                    new IComponent[] { },
                    new IEnchantment[] { });

                foreach (var requiredResourceKvp in skillResourceUsageBehavior.StaticStatRequirements)
                {
                    var requiredStatDefinitionId = requiredResourceKvp.Key;
                    var requiredStatValue = requiredResourceKvp.Value;

                    var actualStatValue = _statCalculationService.GetStatValue(
                        actor,
                        requiredStatDefinitionId,
                        statCalculationContext);

                    if (actualStatValue < requiredStatValue)
                    {
                        _logger.Debug(
                            $"'{actor}' did not meet required stat ID " +
                            $"'{requiredStatDefinitionId}' value of " +
                            $"{requiredStatValue}. Had value of " +
                            $"{actualStatValue}.");
                        return false;
                    }
                }                
            }

            return true;
        }

        public void UseRequiredResources(
            IGameObject actor,
            IGameObject skill)
        {
            if (!skill.TryGetFirst<ISkillResourceUsageBehavior>(out var skillResourceUsageBehavior) ||
                !skillResourceUsageBehavior.StaticStatRequirements.Any())
            {
                return;
            }

            var actorMutableStats = actor.GetOnly<IHasMutableStatsBehavior>();
            actorMutableStats.MutateStats(stats =>
            {
                foreach (var requiredResourceKvp in skillResourceUsageBehavior.StaticStatRequirements)
                {
                    var requiredStatDefinitionId = requiredResourceKvp.Key;
                    var requiredStatValue = requiredResourceKvp.Value;

                    stats[requiredStatDefinitionId] -= requiredStatValue;
                }
            });
        }

        public void ApplySkillEffectsToTarget(
            IGameObject skill,
            IGameObject target)
        {
            var targetEnchantmentsBehavior = target.GetOnly<IHasEnchantmentsBehavior>();
            var skillDefinitionId = skill
                .GetOnly<ITemplateIdentifierBehavior>()
                .TemplateId;
            var statefulEnchantments = _skillDefinitionRepositoryFacade
                .GetSkillDefinitionStatefulEnchantments(skillDefinitionId);
            targetEnchantmentsBehavior.AddEnchantments(statefulEnchantments);
        }
    }
}
