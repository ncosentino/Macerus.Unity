using System.Linq;

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
            bool canAct =
                !_combatTurnManager.InCombat ||
                _combatTurnManager
                    .GetSnapshot(_filterContextProvider.GetContext(), 1)
                    .Single() == actor;
            return canAct;
        }
    }
}
