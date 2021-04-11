using Assets.Scripts.Plugins.Features.Controls;
using Assets.Scripts.Plugins.Features.Maps.Api;

using ProjectXyz.Plugins.Features.Combat.Api;

namespace Assets.Scripts.Plugins.Features.Combat
{
    public sealed class CombatController
    {
        private readonly IObservableCombatTurnManager _combatTurnManager;
        private readonly IMapGridLineFormatter _mapGridLineFormatter;
        private readonly IPlayerControlConfiguration _playerControlsConfiguration;

        public CombatController(
            IObservableCombatTurnManager combatTurnManager,
            IMapGridLineFormatter mapGridLineFormatter,
            IPlayerControlConfiguration playerControlsConfiguration)
        {
            _combatTurnManager = combatTurnManager;
            _mapGridLineFormatter = mapGridLineFormatter;
            _playerControlsConfiguration = playerControlsConfiguration;
            _combatTurnManager.CombatStarted += CombatTurnManager_CombatStarted;
            _combatTurnManager.CombatEnded += CombatTurnManager_CombatEnded;
        }

        private void CombatTurnManager_CombatEnded(
            object sender,
            CombatEndedEventArgs e)
        {
            _mapGridLineFormatter.ToggleGridLines(false);
            _playerControlsConfiguration.TileRestrictedMovement = false;
        }

        private void CombatTurnManager_CombatStarted(
            object sender, 
            CombatStartedEventArgs e)
        {
            _mapGridLineFormatter.ToggleGridLines(true);
            _playerControlsConfiguration.TileRestrictedMovement = true;
        }
    }
}
