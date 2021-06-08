using System.Linq;

using Assets.Scripts.Plugins.Features.Controls;
using Assets.Scripts.Plugins.Features.Maps.Api;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Mapping;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Combat
{
    public sealed class CombatController
    {
        private readonly IObservableCombatTurnManager _combatTurnManager;
        private readonly IMapGridLineFormatter _mapGridLineFormatter;
        private readonly IMapHoverSelectFormatter _mapHoverSelectFormatter;
        private readonly IMapTraversableHighlighter _mapTraversableHighlighter;
        private readonly IPlayerControlConfiguration _playerControlsConfiguration;
        private readonly IMappingAmenity _mappingAmenity;

        public CombatController(
            IObservableCombatTurnManager combatTurnManager,
            IMapGridLineFormatter mapGridLineFormatter,
            IMapHoverSelectFormatter mapHoverSelectFormatter,
            IMapTraversableHighlighter mapTraversableHighlighter,
            IPlayerControlConfiguration playerControlsConfiguration,
            IMappingAmenity mappingAmenity)
        {
            _combatTurnManager = combatTurnManager;
            _mapGridLineFormatter = mapGridLineFormatter;
            _mapHoverSelectFormatter = mapHoverSelectFormatter;
            _mapTraversableHighlighter = mapTraversableHighlighter;
            _playerControlsConfiguration = playerControlsConfiguration;
            _mappingAmenity = mappingAmenity;
            _combatTurnManager.CombatStarted += CombatTurnManager_CombatStarted;
            _combatTurnManager.CombatEnded += CombatTurnManager_CombatEnded;
            _combatTurnManager.TurnProgressed += CombatTurnManager_TurnProgressed;
        }

        private void CombatTurnManager_TurnProgressed(
            object sender,
            TurnProgressedEventArgs e)
        {
            UpdateTraversableHighlighting(e.ActorWithNextTurn);
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
            UpdateTraversableHighlighting(e.ActorOrder.First());
        }

        private void UpdateTraversableHighlighting(IGameObject actor)
        {
            var traversablePoints = actor.Has<IPlayerControlledBehavior>()
                ? _mappingAmenity.GetAllowedPathDestinationsForActor(actor)
                : Enumerable.Empty<System.Numerics.Vector2>();
            _mapTraversableHighlighter.SetTraversableTiles(traversablePoints.Select(p => new Vector2Int((int)p.X, (int)p.Y)));
        }
    }
}
