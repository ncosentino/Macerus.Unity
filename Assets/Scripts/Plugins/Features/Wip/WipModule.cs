using System.Linq;

using Autofac;

using ProjectXyz.Api.Logging;
using ProjectXyz.Plugins.Features.Combat.Api;

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
