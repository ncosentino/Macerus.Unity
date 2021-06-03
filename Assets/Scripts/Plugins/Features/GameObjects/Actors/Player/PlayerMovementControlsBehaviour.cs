using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Gui;
using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.Controls;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.Input;

using Macerus.Api.Behaviors;

using NexusLabs.Contracts;

using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerMovementControlsBehaviour :
        MonoBehaviour,
        IPointerUpHandler,
        IPointerDownHandler
    {
        public IDebugConsoleManager DebugConsoleManager { get; set; }

        public IKeyboardControls KeyboardControls { get; set; }

        public IKeyboardInput KeyboardInput { get; set; }

        public IMouseInput MouseInput { get; set; }

        public IGuiHitTester GuiHitTester { get; set; }

        public IReadOnlySizeBehavior SizeBehavior { get; set; }

        public IReadOnlyPositionBehavior PositionBehavior { get; set; }

        public IMovementBehavior MovementBehavior { get; set; }

        public ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        public IScreenPointToMapCellConverter ScreenPointToMapCellConverter { get; set; }

        public IPlayerControlConfiguration PlayerControlConfiguration { get; set; }

        public IMapManager MapManager { get; set; }

        private void Start()
        {
            this.RequiresNotNull(KeyboardControls, nameof(KeyboardControls));
            this.RequiresNotNull(KeyboardInput, nameof(KeyboardInput));
            this.RequiresNotNull(MouseInput, nameof(MouseInput));
            this.RequiresNotNull(MovementBehavior, nameof(MovementBehavior));
            this.RequiresNotNull(PositionBehavior, nameof(PositionBehavior));
            this.RequiresNotNull(SizeBehavior, nameof(SizeBehavior));
            this.RequiresNotNull(Logger, nameof(Logger));
            this.RequiresNotNull(DebugConsoleManager, nameof(DebugConsoleManager));
            this.RequiresNotNull(GuiHitTester, nameof(GuiHitTester));
            this.RequiresNotNull(ScreenPointToMapCellConverter, nameof(ScreenPointToMapCellConverter));
            this.RequiresNotNull(PlayerControlConfiguration, nameof(PlayerControlConfiguration));
            this.RequiresNotNull(MapManager, nameof(MapManager));
        }

        private void Update()
        {
            HandleMovementControls();
        }

        private void HandleMovementControls()
        {
            if (DebugConsoleManager.GetConsoleWindowVisible())
            {
                return;
            }

            if (PlayerControlConfiguration.MouseMovementEnabled && MouseInput.GetMouseButtonDown(0))
            {
                if (!GuiHitTester.HitTest(MouseInput.Position).Any())
                {
                    IReadOnlyCollection<System.Numerics.Vector2> path;
                    Vector3 worldLocation;
                    if (PlayerControlConfiguration.TileRestrictedMovement)
                    {
                        worldLocation = ScreenPointToMapCellConverter.Convert(MouseInput.Position);
                        path = MapManager
                            .PathFinder
                            .FindPath(
                                new System.Numerics.Vector2((float)PositionBehavior.X, (float)PositionBehavior.Y),
                                new System.Numerics.Vector2(worldLocation.x, worldLocation.y),
                                new System.Numerics.Vector2((float)SizeBehavior.Width, (float)SizeBehavior.Height))
                            .ToArray();

                        if (path.Any())
                        {
                            Logger.Info(
                                $"Path between ({PositionBehavior.X},{PositionBehavior.Y}) and ({worldLocation.x},{worldLocation.y}):\r\n" +
                                $"{string.Join("\r\n", path.Select(p => $"\t({p.X},{p.Y})"))}");
                        }
                        else
                        {
                            Logger.Warn(
                                $"Could not find a path from ({PositionBehavior.X}," +
                                $"{PositionBehavior.Y}) to ({worldLocation.x}," +
                                $"{worldLocation.y}). Using the way of the crow.");
                            path = new[]
                            {
                                new System.Numerics.Vector2(worldLocation.x, worldLocation.y)
                            };
                        }
                    }
                    else
                    {
                        worldLocation = Camera.main.ScreenToWorldPoint(MouseInput.Position);
                        path = new[]
                        {
                            new System.Numerics.Vector2(worldLocation.x, worldLocation.y)
                        };
                    }

                    path = new[] { new System.Numerics.Vector2((float)PositionBehavior.X, (float)PositionBehavior.Y) }
                        .Concat(path)
                        .ToArray();
                    MovementBehavior.SetWalkPath(path);
                }
            }

            if (PlayerControlConfiguration.KeyboardMovementEnabled)
            {
                double throttleY;
                if (KeyboardInput.GetKey(KeyboardControls.MoveDown))
                {
                    throttleY = -1;
                }
                else if (KeyboardInput.GetKey(KeyboardControls.MoveUp))
                {
                    throttleY = 1;
                }
                else if (MovementBehavior.PointsToWalk.Count < 1)
                {
                    throttleY = 0;
                }
                else
                {
                    throttleY = MovementBehavior.ThrottleY;
                }

                double throttleX;
                if (KeyboardInput.GetKey(KeyboardControls.MoveLeft))
                {
                    throttleX = -1;
                }
                else if (KeyboardInput.GetKey(KeyboardControls.MoveRight))
                {
                    throttleX = 1;
                }
                else if (MovementBehavior.PointsToWalk.Count < 1)
                {
                    throttleX = 0;
                }
                else
                {
                    throttleX = MovementBehavior.ThrottleX;
                }

                MovementBehavior.SetThrottle(throttleX, throttleY);
            }
        }


        public void OnPointerDown(PointerEventData eventData)
        {

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            if (eventData.pointerCurrentRaycast.gameObject == null)
            {

            }
        }
    }
}
