using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.Controls;
using Assets.Scripts.Unity.Input;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class KeyboardMovementHandler : IKeyboardMovementHandler
    {
        private readonly IPlayerControlConfiguration _playerControlConfiguration;
        private readonly IKeyboardControls _keyboardControls;
        private readonly IKeyboardInput _keyboardInput;

        public KeyboardMovementHandler(
            IPlayerControlConfiguration playerControlConfiguration,
            IKeyboardControls keyboardControls,
            IKeyboardInput keyboardInput)
        {
            _playerControlConfiguration = playerControlConfiguration;
            _keyboardControls = keyboardControls;
            _keyboardInput = keyboardInput;
        }

        public void HandleKeyboardMovement(IGameObject actor)
        {
            if (!_playerControlConfiguration.KeyboardMovementEnabled)
            {
                return;
            }

            var movementBehavior = actor.GetOnly<IMovementBehavior>();

            double throttleY;
            if (_keyboardInput.GetKey(_keyboardControls.MoveDown))
            {
                throttleY = -1;
            }
            else if (_keyboardInput.GetKey(_keyboardControls.MoveUp))
            {
                throttleY = 1;
            }
            else if (movementBehavior.PointsToWalk.Count < 1)
            {
                throttleY = 0;
            }
            else
            {
                throttleY = movementBehavior.ThrottleY;
            }

            double throttleX;
            if (_keyboardInput.GetKey(_keyboardControls.MoveLeft))
            {
                throttleX = -1;
            }
            else if (_keyboardInput.GetKey(_keyboardControls.MoveRight))
            {
                throttleX = 1;
            }
            else if (movementBehavior.PointsToWalk.Count < 1)
            {
                throttleX = 0;
            }
            else
            {
                throttleX = movementBehavior.ThrottleX;
            }

            movementBehavior.SetThrottle(throttleX, throttleY);
        }
    }
}
