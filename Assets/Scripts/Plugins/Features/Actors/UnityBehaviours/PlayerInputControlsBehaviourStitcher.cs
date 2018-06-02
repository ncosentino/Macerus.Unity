using Assets.Scripts.Api.Scenes.Explore;
using Assets.Scripts.Scenes.Explore.GameObjects;
using Assets.Scripts.Unity.GameObjects;
using Macerus.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public sealed class PlayerInputControlsBehaviourStitcher : IPlayerInputControlsBehaviourStitcher
    {
        private readonly IKeyboardControls _keyboardControls;

        public PlayerInputControlsBehaviourStitcher(IKeyboardControls keyboardControls)
        {
            _keyboardControls = keyboardControls;
        }

        public void Attach(GameObject gameObject)
        {
            var playerInputControlsBehaviour = gameObject.AddComponent<PlayerInputControlsBehaviour>();
            playerInputControlsBehaviour.KeyboardControls = _keyboardControls;
            playerInputControlsBehaviour.MovementBehavior = gameObject
                .GetRequiredComponent<HasGameObjectBehaviour>()
                .GameObject
                .GetOnly<IMovementBehavior>();
        }
    }
}