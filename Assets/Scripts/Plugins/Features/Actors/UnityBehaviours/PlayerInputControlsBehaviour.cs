using Assets.Scripts.Api.Scenes.Explore;
using Macerus.Api.Behaviors;
using ProjectXyz.Framework.Contracts;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public sealed class PlayerInputControlsBehaviour : 
        MonoBehaviour,
        IPlayerInputControlsBehaviour
    {
        public IKeyboardControls KeyboardControls { get; set; }

        public IMovementBehavior MovementBehavior { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                KeyboardControls,
                $"{nameof(KeyboardControls)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                MovementBehavior,
                $"{nameof(MovementBehavior)} was not set on '{gameObject}.{this}'.");
        }

        private void Update()
        {
            HandleMovementControls();
        }

        private void HandleMovementControls()
        {
            float throttleY;
            if (Input.GetKey(KeyboardControls.MoveDown))
            {
                throttleY = -1;
            }
            else if (Input.GetKey(KeyboardControls.MoveUp))
            {
                throttleY = 1;
            }
            else
            {
                throttleY = 0;
            }

            float throttleX;
            if (Input.GetKey(KeyboardControls.MoveLeft))
            {
                throttleX = -1;
            }
            else if (Input.GetKey(KeyboardControls.MoveRight))
            {
                throttleX = 1;
            }
            else
            {
                throttleX = 0;
            }

            if (Mathf.Abs(throttleX) > float.Epsilon &&
                Mathf.Abs(throttleY) > float.Epsilon)
            {
                throttleX /= 2;
                throttleY /= 2;
            }

            if (Mathf.Abs(throttleX) > float.Epsilon)
            {
                MovementBehavior.ThrottleX += throttleX;
                Debug.Log($"'{MovementBehavior}' XThrottle: {MovementBehavior.ThrottleX}");
            }

            if (Mathf.Abs(throttleY) > float.Epsilon)
            {
                MovementBehavior.ThrottleY += throttleY;
                Debug.Log($"'{MovementBehavior}' YThrottle: {MovementBehavior.ThrottleY}");
            }
        }
    }
}
