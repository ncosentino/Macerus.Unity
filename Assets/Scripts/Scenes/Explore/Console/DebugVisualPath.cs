using System.Collections.Generic;
using System.Linq;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Console
{
    public sealed class DebugVisualPathBehaviour : MonoBehaviour
    {
        public IReadOnlyCollection<System.Numerics.Vector2> Points { get; set; }

        private void Start()
        {
            this.RequiresNotNull(Points, nameof(Points));

            var pointsForLine = Points.Select(x => new Vector3(x.X, x.Y)).ToArray();
            var lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = pointsForLine.Length;
            lineRenderer.SetPositions(pointsForLine);
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.green;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.sortingOrder = 1;
        }
    }
}
