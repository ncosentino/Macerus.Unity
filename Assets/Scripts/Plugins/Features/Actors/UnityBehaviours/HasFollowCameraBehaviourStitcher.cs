using System.Linq;
using Assets.Scripts.Scenes.Explore;
using Assets.Scripts.Scenes.Explore.Camera;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public sealed class HasFollowCameraBehaviourStitcher : IHasFollowCameraBehaviourStitcher
    {
        private readonly IGameObjectManager _gameObjectManager;
        private readonly ICameraFactory _cameraFactory;
        private readonly IObjectDestroyer _objectDestroyer;

        public HasFollowCameraBehaviourStitcher(
            IGameObjectManager gameObjectManager,
            ICameraFactory cameraFactory,
            IObjectDestroyer objectDestroyer)
        {
            _gameObjectManager = gameObjectManager;
            _cameraFactory = cameraFactory;
            _objectDestroyer = objectDestroyer;
        }

        public IReadOnlyHasFollowCameraBehaviour Attach(GameObject gameObject)
        {
            var hasFollowCameraBehaviour = gameObject.AddComponent<HasFollowCameraBehaviour>();
            hasFollowCameraBehaviour.CameraFactory = _cameraFactory;
            hasFollowCameraBehaviour.ObjectDestroyer = _objectDestroyer;

            // FIXME: this is a filthy hack.
            hasFollowCameraBehaviour.ExploreGameObject = _gameObjectManager.FindAll().First(x => x.HasRequiredComponent<ExploreSceneBehaviour>());
            return hasFollowCameraBehaviour;
        }
    }
}