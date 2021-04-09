using Assets.Scripts.Unity;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors
{
    public sealed class DrawWalkPathBehaviourStitcher : IDrawWalkPathBehaviourStitcher
    {
        private readonly ITimeProvider _timeProvider;

        public DrawWalkPathBehaviourStitcher(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public IDrawWalkPathBehaviour Stitch(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var drawWalkPathBehaviour = unityGameObject.AddComponent<DrawWalkPathBehaviour>();
            drawWalkPathBehaviour.TimeProvider = _timeProvider;
            drawWalkPathBehaviour.MovementBehavior = gameObject.GetOnly<IReadOnlyMovementBehavior>();
            drawWalkPathBehaviour.WorldLocationBehavior = gameObject.GetOnly<IReadOnlyWorldLocationBehavior>();

            drawWalkPathBehaviour.LineRenderer = unityGameObject.AddComponent<LineRenderer>();
            drawWalkPathBehaviour.LineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
            drawWalkPathBehaviour.LineRenderer.startColor = Color.green;
            drawWalkPathBehaviour.LineRenderer.endColor = Color.yellow;
            drawWalkPathBehaviour.LineRenderer.startWidth = 0.02f;
            drawWalkPathBehaviour.LineRenderer.endWidth = 0.02f;

            return drawWalkPathBehaviour;
        }
    }
}
