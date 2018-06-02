using Assets.Scripts.Unity.Threading;
using Macerus.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public sealed class WorldLocationStitcher : IWorldLocationStitcher
    {
        private readonly IDispatcher _dispatcher;

        public WorldLocationStitcher(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public IUpdateWorldLocatiobBehaviour Stitch(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var observableWorldLocationBehavior = gameObject.GetOnly<IObservableWorldLocationBehavior>();
            var updateWorldLocationBehaviour = unityGameObject.AddComponent<UpdateWorldLocationBehaviour>();
            updateWorldLocationBehaviour.ObservableWorldLocationBehavior = observableWorldLocationBehavior;
            updateWorldLocationBehaviour.Dispatcher = _dispatcher;
            return updateWorldLocationBehaviour;
        }
    }
}