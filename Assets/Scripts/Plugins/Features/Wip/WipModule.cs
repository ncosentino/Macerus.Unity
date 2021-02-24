using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
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
        private readonly ICalculationPriorityFactory _calculationPriorityFactory;
        private readonly IEnchantmentFactory _enchantmentFactory;
        private readonly IStatDefinitionToTermConverter _statDefinitionToTermConverter;

        public WipSkills(
            ICalculationPriorityFactory calculationPriorityFactory,
            IEnchantmentFactory enchantmentFactory,
            IStatDefinitionToTermConverter statDefinitionToTermConverter)
        {
            _calculationPriorityFactory = calculationPriorityFactory;
            _enchantmentFactory = enchantmentFactory;
            _statDefinitionToTermConverter = statDefinitionToTermConverter;
        }

        public void ApplySkillEffectsToTarget(
            IGameObject skill,
            IGameObject target)
        {
            var targetEnchantmentsBehavior = target.GetOnly<IHasEnchantmentsBehavior>();

            var skillStatEnchantments = skill
                .GetOnly<IHasStatsBehavior>()
                .BaseStats
                .Select(statKvp =>
                {
                    var statDefinitionid = statKvp.Key;
                    var statValue = statKvp.Value;
                    var statTerm = _statDefinitionToTermConverter[statDefinitionid];
                    var skillStatEnchantment = _enchantmentFactory.Create(
                        new IBehavior[]
                        {
                            new EnchantmentTargetBehavior(new StringIdentifier("self")),
                            new HasStatDefinitionIdBehavior()
                            {
                                StatDefinitionId = statDefinitionid,
                            },
                            new EnchantmentExpressionBehavior(
                                _calculationPriorityFactory.Create<int>(-1),
                                $"{statTerm} + {statValue}")
                        });
                    return skillStatEnchantment;
                })
                .ToArray();
            // FIXME: skill enchantments need to be re-created every time we 
            // want to use them if we want to add them to another collection. 
            // this is because if they expire, they'll be removed from their
            // original source skill
            var applicableSkillEnchantments = skill
                .GetOnly<IHasReadOnlyEnchantmentsBehavior>()
                .Enchantments
                .Concat(skillStatEnchantments)
                .ToArray();
            targetEnchantmentsBehavior.AddEnchantments(applicableSkillEnchantments);
        }
    }
}
