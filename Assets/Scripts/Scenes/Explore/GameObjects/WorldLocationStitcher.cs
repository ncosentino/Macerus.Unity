using Macerus.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public sealed class WorldLocationStitcher : IWorldLocationStitcher
    {
        public IUpdateWorldLocatiobBehaviour Stitch(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var observableWorldLocationBehavior = gameObject.GetOnly<IObservableWorldLocationBehavior>();
            var updateWorldLocationBehaviour = unityGameObject.AddComponent<UpdateWorldLocationBehaviour>();
            updateWorldLocationBehaviour.ObservableWorldLocationBehavior = observableWorldLocationBehavior;
            return updateWorldLocationBehaviour;
        }
    }
}