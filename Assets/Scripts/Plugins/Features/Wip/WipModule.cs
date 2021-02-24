using System.Linq;

using Autofac;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
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
            var applicableSkillEnchantments = skill
                .GetOnly<IHasReadOnlyEnchantmentsBehavior>()
                .Enchantments
                .Concat(skillStatEnchantments)
                .ToArray();
            targetEnchantmentsBehavior.AddEnchantments(applicableSkillEnchantments);
        }
    }
}
