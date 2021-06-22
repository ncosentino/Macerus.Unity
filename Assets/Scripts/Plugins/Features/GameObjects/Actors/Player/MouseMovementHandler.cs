using System;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Gui;
using Assets.Scripts.Plugins.Features.Controls;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.Input;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.Mapping;
using Macerus.Plugins.Features.Stats.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class MouseMovementHandler : IMouseMovementHandler
    {
        private readonly IPlayerControlConfiguration _playerControlConfiguration;
        private readonly IMouseInput _mouseInput;
        private readonly IGuiHitTester _guiHitTester;
        private readonly IMapManager _mapManager;
        private readonly ProjectXyz.Api.Logging.ILogger _logger;
        private readonly IScreenPointToMapCellConverter _screenPointToMapCellConverter;
        private readonly IMacerusActorIdentifiers _actorIdentifiers;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly IMappingAmenity _mappingAmenity;

        public MouseMovementHandler(
            IPlayerControlConfiguration playerControlConfiguration, 
            IMouseInput mouseInput, 
            IGuiHitTester guiHitTester, 
            IMapManager mapManager, 
            ProjectXyz.Api.Logging.ILogger logger,
            IScreenPointToMapCellConverter screenPointToMapCellConverter,
            IMacerusActorIdentifiers actorIdentifiers,
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            IMappingAmenity mappingAmenity)
        {
            _playerControlConfiguration = playerControlConfiguration;
            _mouseInput = mouseInput;
            _guiHitTester = guiHitTester;
            _mapManager = mapManager;
            _logger = logger;
            _screenPointToMapCellConverter = screenPointToMapCellConverter;
            _actorIdentifiers = actorIdentifiers;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _mappingAmenity = mappingAmenity;
        }

        public void HandleMouseMovement(IGameObject actor)
        {
            if (!_playerControlConfiguration.MouseMovementEnabled ||
                !_mouseInput.GetMouseButtonDown(0))
            {
                return;
            }

            if (_guiHitTester.HitTest(_mouseInput.Position).Any())
            {
                return;
            }

            var movementBehavior = actor.GetOnly<IMovementBehavior>();
            var positionBehavior = actor.GetOnly<IReadOnlyPositionBehavior>();
            var sizeBehavior = actor.GetOnly<IReadOnlySizeBehavior>();

            IReadOnlyCollection<System.Numerics.Vector2> path;
            Vector3 destinationPosition;
            if (_playerControlConfiguration.TileRestrictedMovement)
            {
                if (Math.Abs(movementBehavior.VelocityX) > 0 ||
                    Math.Abs(movementBehavior.VelocityY) > 0)
                {
                    return;
                }

                var allowedWalkDiagonally = _statCalculationServiceAmenity.GetStatValue(
                    movementBehavior.Owner,
                    _actorIdentifiers.MoveDiagonallyStatDefinitionId) > 0;
                var allowedWalkDistance = _statCalculationServiceAmenity.GetStatValue(
                    movementBehavior.Owner,
                    _actorIdentifiers.MoveDistancePerTurnCurrentStatDefinitionId);
                var actorPosition = new System.Numerics.Vector2(
                    (float)positionBehavior.X,
                    (float)positionBehavior.Y);
                var actorSize = new System.Numerics.Vector2(
                    (float)sizeBehavior.Width,
                    (float)sizeBehavior.Height);

                var validWalkPoints = _mappingAmenity.GetAllowedPathDestinationsForActor(actor);

                destinationPosition = _screenPointToMapCellConverter.Convert(_mouseInput.Position);
                if (!validWalkPoints.Contains(new System.Numerics.Vector2(
                    destinationPosition.x,
                    destinationPosition.y)))
                {
                    return;
                }

                var foundPath = _mapManager
                    .PathFinder
                    .FindPath(
                        actorPosition,
                        new System.Numerics.Vector2(destinationPosition.x, destinationPosition.y),
                        actorSize,
                        allowedWalkDiagonally);
                Contract.Requires(
                    foundPath.TotalDistance <= allowedWalkDistance,
                    $"The found path was distance {foundPath.TotalDistance} " +
                    $"but the maximum allowed distance for the actor to move was " +
                    $"{allowedWalkDistance}. This may suggest that " +
                    $"{nameof(IPathFinder.GetAllowedPathDestinations)}() is " +
                    $"doing the wrong thing.");
                
                if (!foundPath.Positions.Any())
                {
                    _logger.Debug(
                        $"Could not find a path from ({positionBehavior.X}," +
                        $"{positionBehavior.Y}) to ({destinationPosition.x}," +
                        $"{destinationPosition.y}).");
                    return;
                }

                path = foundPath.Positions;
                _logger.Info(
                    $"Path between ({positionBehavior.X},{positionBehavior.Y}) and ({destinationPosition.x},{destinationPosition.y}):\r\n" +
                    $"{string.Join("\r\n", path.Select(p => $"\t({p.X},{p.Y})"))}");
                
                // FIXME: actually calculate the path distance (i.e. account for things like diagonals)
                actor
                    .GetOnly<IHasMutableStatsBehavior>()
                    .MutateStats(stats => stats[_actorIdentifiers.MoveDistancePerTurnCurrentStatDefinitionId] -= foundPath.TotalDistance);
            }
            else
            {
                destinationPosition = Camera.main.ScreenToWorldPoint(_mouseInput.Position);
                path = new[]
                {
                    new System.Numerics.Vector2(destinationPosition.x, destinationPosition.y)
                };
            }

            path = new[] { new System.Numerics.Vector2((float)positionBehavior.X, (float)positionBehavior.Y) }
                .Concat(path)
                .ToArray();
            movementBehavior.SetWalkPath(path);
        }
    }
}
