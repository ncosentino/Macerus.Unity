using Assets.Scripts.Plugins.Features.Maps.Api;

using ProjectXyz.Plugins.Features.Combat.Api;

namespace Assets.Scripts.Plugins.Features.Combat
{
    public sealed class CombatController
    {
        private readonly IObservableCombatTurnManager _combatTurnManager;
        private readonly IMapGridLineFormatter _mapGridLineFormatter;

        public CombatController(
            IObservableCombatTurnManager combatTurnManager,
            IMapGridLineFormatter mapGridLineFormatter)
        {
            _combatTurnManager = combatTurnManager;
            _mapGridLineFormatter = mapGridLineFormatter;
            _combatTurnManager.CombatStarted += CombatTurnManager_CombatStarted;
            _combatTurnManager.CombatEnded += CombatTurnManager_CombatEnded;
        }

        private void CombatTurnManager_CombatEnded(
            object sender,
            CombatEndedEventArgs e)
        {
            _mapGridLineFormatter.ToggleGridLines(false);
        }

        private void CombatTurnManager_CombatStarted(
            object sender, 
            CombatStartedEventArgs e)
        {
            _mapGridLineFormatter.ToggleGridLines(true);
        }
    }
}
