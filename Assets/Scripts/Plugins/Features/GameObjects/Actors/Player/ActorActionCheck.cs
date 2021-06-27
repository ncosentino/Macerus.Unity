using System.Linq;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class ActorActionCheck : IActorActionCheck
    {
        private readonly IReadOnlyCombatTurnManager _combatTurnManager;
        private readonly IFilterContextProvider _filterContextProvider;

        public ActorActionCheck(
            IReadOnlyCombatTurnManager combatTurnManager,
            IFilterContextProvider filterContextProvider)
        {
            _combatTurnManager = combatTurnManager;
            _filterContextProvider = filterContextProvider;
        }

        public bool CanAct(IGameObject actor)
        {
            var isActivePlayerControlled =
                actor.TryGetFirst<IPlayerControlledBehavior>(out var playerControlledBehavior) &&
                playerControlledBehavior.IsActive;
            var isCurrentCombatTurn =
                _combatTurnManager.InCombat &&
                _combatTurnManager
                    .GetSnapshot(_filterContextProvider.GetContext(), 1)
                    .Single() == actor;
            bool canAct =
                isActivePlayerControlled &&
                (!_combatTurnManager.InCombat || isCurrentCombatTurn);
            return canAct;
        }
    }
}
