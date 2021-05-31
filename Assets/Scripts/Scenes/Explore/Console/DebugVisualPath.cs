using System;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Unity;
using Assets.Scripts.Unity.GameObjects;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Console
{
    public sealed class DebugVisualPathBehaviour : MonoBehaviour
    {
        private float _triggerTime;

        public ITimeProvider TimeProvider { get; set; }

        public IObjectDestroyer ObjectDestroyer { get; set; }

        public IReadOnlyCollection<System.Numerics.Vector2> Points { get; set; }

        public TimeSpan Duration { get; set; }

        private void Start()
        {
            this.RequiresNotNull(Points, nameof(Points));
            this.RequiresNotNull(TimeProvider, nameof(TimeProvider));
            this.RequiresNotNull(ObjectDestroyer, nameof(ObjectDestroyer));

            var pointsForLine = Points.Select(x => new Vector3(x.X, x.Y)).ToArray();
            var lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = pointsForLine.Length;
            lineRenderer.SetPositions(pointsForLine);
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.green;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.sortingOrder = 1;

            _triggerTime = TimeProvider.SecondsSinceStartOfGame + (float)Duration.TotalSeconds;
        }

        private void FixedUpdate()
        {
            if (TimeProvider.SecondsSinceStartOfGame < _triggerTime)
            {
                return;
            }

            ObjectDestroyer.Destroy(gameObject);
        }
    }
}
