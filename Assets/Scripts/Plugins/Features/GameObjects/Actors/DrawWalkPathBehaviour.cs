using System.Linq;

using Assets.Scripts.Unity;

using Macerus.Api.Behaviors;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors
{
    public sealed class DrawWalkPathBehaviour :
        MonoBehaviour,
        IDrawWalkPathBehaviour
    {
        private float _triggerTime;

        public IReadOnlyMovementBehavior MovementBehavior { get; set; }

        public IReadOnlyWorldLocationBehavior WorldLocationBehavior { get; set; }

        public ITimeProvider TimeProvider { get; set; }

        public LineRenderer LineRenderer { get; set; }

        private void Start()
        {
            this.RequiresNotNull(MovementBehavior, nameof(MovementBehavior));
            this.RequiresNotNull(WorldLocationBehavior, nameof(WorldLocationBehavior));
            this.RequiresNotNull(LineRenderer, nameof(LineRenderer));
            this.RequiresNotNull(TimeProvider, nameof(TimeProvider));
        }

        private void FixedUpdate()
        {
            if (TimeProvider.SecondsSinceStartOfGame < _triggerTime)
            {
                return;
            }

            ResetTriggerTime();

            var points = MovementBehavior.PointsToWalk.Any()
                ? new Vector3(
                    (float)WorldLocationBehavior.X,
                    (float)WorldLocationBehavior.Y,
                    -1)
                    .Yield()
                    .Concat(MovementBehavior
                        .PointsToWalk
                        .Select(p => new Vector3(p.X, p.Y, -1)))
                    .ToArray()
                : new Vector3[0];

            LineRenderer.SetPositions(points);
            LineRenderer.positionCount = points.Length;
        }

        private void ResetTriggerTime()
        {
            _triggerTime = TimeProvider.SecondsSinceStartOfGame + 0.1f;
        }
    }
}
