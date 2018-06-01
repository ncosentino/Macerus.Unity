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

        public IWorldLocationBehavior WorldLocationBehavior { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                KeyboardControls,
                $"{nameof(KeyboardControls)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                WorldLocationBehavior,
                $"{nameof(WorldLocationBehavior)} was not set on '{gameObject}.{this}'.");
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyboardControls.MoveDown))
            {
                Debug.Log("Move down.");
                WorldLocationBehavior.Y -= 1;
            }
            else if (Input.GetKeyUp(KeyboardControls.MoveUp))
            {
                Debug.Log("Move up.");
                WorldLocationBehavior.Y += 1;
            }
            else if (Input.GetKeyUp(KeyboardControls.MoveLeft))
            {
                Debug.Log("Move left.");
                WorldLocationBehavior.X -= 1;
            }
            else if (Input.GetKeyUp(KeyboardControls.MoveRight))
            {
                Debug.Log("Move right.");
                WorldLocationBehavior.X += 1;
            }
        }
    }
}
