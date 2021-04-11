using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Gui;
using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.Input;

using Macerus.Api.Behaviors;

using NexusLabs.Contracts;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerMovementControlsBehaviour :
        MonoBehaviour,
        IPlayerMovementControlsBehaviour,
        IPointerUpHandler,
        IPointerDownHandler
    {
        public IDebugConsoleManager DebugConsoleManager { get; set; }

        public IKeyboardControls KeyboardControls { get; set; }

        public IKeyboardInput KeyboardInput { get; set; }

        public IMouseInput MouseInput { get; set; }

        public IGuiHitTester GuiHitTester { get; set; }

        public IMovementBehavior MovementBehavior { get; set; }

        public ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, KeyboardControls, nameof(KeyboardControls));
            UnityContracts.RequiresNotNull(this, KeyboardInput, nameof(KeyboardInput));
            UnityContracts.RequiresNotNull(this, MouseInput, nameof(MouseInput));
            UnityContracts.RequiresNotNull(this, MovementBehavior, nameof(MovementBehavior));
            UnityContracts.RequiresNotNull(this, Logger, nameof(Logger));
            UnityContracts.RequiresNotNull(this, DebugConsoleManager, nameof(DebugConsoleManager));
            UnityContracts.RequiresNotNull(this, GuiHitTester, nameof(GuiHitTester));
        }

        private void FixedUpdate()
        {
            HandleMovementControls();
        }

        private void HandleMovementControls()
        {
            if (MouseInput.GetMouseButtonDown(0))
            {
                if (!GuiHitTester.HitTest(MouseInput.Position).Any())
                {
                    var worldLocation = Camera.main.ScreenToWorldPoint(MouseInput.Position);
                    // FIXME: actually generate a path here, not just a single point
                    MovementBehavior.SetWalkPath(new[]
                    {
                        new System.Numerics.Vector2(worldLocation.x, worldLocation.y)
                    });
                }
            }

            if (DebugConsoleManager.GetConsoleWindowVisible())
            {
                return;
            }

            double throttleY;
            if (KeyboardInput.GetKey(KeyboardControls.MoveDown))
            {
                throttleY = -1;
            }
            else if (KeyboardInput.GetKey(KeyboardControls.MoveUp))
            {
                throttleY = 1;
            }
            else if(MovementBehavior.PointsToWalk.Count < 1)
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
