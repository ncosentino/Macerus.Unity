using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Gui;
using Assets.Scripts.Plugins.Features.Controls;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.Input;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;

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

        public MouseMovementHandler(
            IPlayerControlConfiguration playerControlConfiguration, 
            IMouseInput mouseInput, 
            IGuiHitTester guiHitTester, 
            IMapManager mapManager, 
            ProjectXyz.Api.Logging.ILogger logger,
            IScreenPointToMapCellConverter screenPointToMapCellConverter)
        {
            _playerControlConfiguration = playerControlConfiguration;
            _mouseInput = mouseInput;
            _guiHitTester = guiHitTester;
            _mapManager = mapManager;
            _logger = logger;
            _screenPointToMapCellConverter = screenPointToMapCellConverter;
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
            Vector3 worldLocation;
            if (_playerControlConfiguration.TileRestrictedMovement)
            {
                //StatCalculationServiceAmenity.GetStatValue(movementBehavior.Owner,

                worldLocation = _screenPointToMapCellConverter.Convert(_mouseInput.Position);
                path = _mapManager
                    .PathFinder
                    .FindPath(
                        new System.Numerics.Vector2((float)positionBehavior.X, (float)positionBehavior.Y),
                        new System.Numerics.Vector2(worldLocation.x, worldLocation.y),
                        new System.Numerics.Vector2((float)sizeBehavior.Width, (float)sizeBehavior.Height))
                    .ToArray();

                if (path.Any())
                {
                    _logger.Info(
                        $"Path between ({positionBehavior.X},{positionBehavior.Y}) and ({worldLocation.x},{worldLocation.y}):\r\n" +
                        $"{string.Join("\r\n", path.Select(p => $"\t({p.X},{p.Y})"))}");
                }
                else
                {
                    _logger.Debug(
                        $"Could not find a path from ({positionBehavior.X}," +
                        $"{positionBehavior.Y}) to ({worldLocation.x}," +
                        $"{worldLocation.y}).");
                    return;
                }
            }
            else
            {
                worldLocation = Camera.main.ScreenToWorldPoint(_mouseInput.Position);
                path = new[]
                {
                        new System.Numerics.Vector2(worldLocation.x, worldLocation.y)
                    };
            }

            path = new[] { new System.Numerics.Vector2((float)positionBehavior.X, (float)positionBehavior.Y) }
                .Concat(path)
                .ToArray();
            movementBehavior.SetWalkPath(path);
        }
    }
}
