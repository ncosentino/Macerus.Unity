using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Plugins.Features.TurnBased.Api;
using ProjectXyz.Shared.Framework;

namespace Assets.Scripts.Plugins.Features.Wip
{
    public sealed class WipModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<CombatTurnLogger>()
                .AutoActivate()
                .AsSelf();
        }
    }

    public sealed class CombatTurnLogger
    {
        public CombatTurnLogger(
            ICombatTurnManager combatTurnManager,
            ILogger logger)
        {
            combatTurnManager.CombatStarted += (_, e) =>
            {
                logger.Info(
                    $"Combat started.\r\n" +
                    $"Turn Order:\r\n" +
                    $"{string.Join("\r\n", e.ActorOrder.Select(x => "\t" + x))}");
            };
            combatTurnManager.TurnProgressed += (_, e) =>
            {
                logger.Info(
                    $"Combat turn progressed.\r\n" +
                    $"Last Turn: {e.ActorTurnProgression.Single()}\r\n" + 
                    $"Next Turn: {e.ActorWithNextTurn}");
            };
        }
    }
}
