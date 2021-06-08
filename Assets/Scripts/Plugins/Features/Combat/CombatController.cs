using Assets.Scripts.Plugins.Features.Controls;

using Macerus.Plugins.Features.Mapping;

using ProjectXyz.Plugins.Features.Combat.Api;

namespace Assets.Scripts.Plugins.Features.Combat
{
    public sealed class CombatController
    {
        private readonly IObservableCombatTurnManager _combatTurnManager;
        private readonly IMapGridLineFormatter _mapGridLineFormatter;
        private readonly IMapHoverSelectFormatter _mapHoverSelectFormatter;
        private readonly IPlayerControlConfiguration _playerControlsConfiguration;

        public CombatController(
            IObservableCombatTurnManager combatTurnManager,
            IMapGridLineFormatter mapGridLineFormatter,
            IMapHoverSelectFormatter mapHoverSelectFormatter,
            IPlayerControlConfiguration playerControlsConfiguration)
        {
            _combatTurnManager = combatTurnManager;
            _mapGridLineFormatter = mapGridLineFormatter;
            _mapHoverSelectFormatter = mapHoverSelectFormatter;
            _playerControlsConfiguration = playerControlsConfiguration;

            _combatTurnManager.CombatStarted += CombatTurnManager_CombatStarted;
            _combatTurnManager.CombatEnded += CombatTurnManager_CombatEnded;
        }

        private void CombatTurnManager_CombatEnded(
            object sender,
            CombatEndedEventArgs e)
        {
            _mapGridLineFormatter.ToggleGridLines(false);
            _mapHoverSelectFormatter.HoverSelectTile(null);
            _playerControlsConfiguration.TileRestrictedMovement = false;
            _playerControlsConfiguration.HoverTileSelection = false;
        }

        private void CombatTurnManager_CombatStarted(
            object sender, 
            CombatStartedEventArgs e)
        {
            _mapGridLineFormatter.ToggleGridLines(true);
            _mapHoverSelectFormatter.HoverSelectTile(null);
            _playerControlsConfiguration.TileRestrictedMovement = true;
            _playerControlsConfiguration.HoverTileSelection = true;
        }
    }
}
