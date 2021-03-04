using NexusLabs.Contracts;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedRain
{
    public sealed class RainAnimationBehaviour : MonoBehaviour
    {
        public float ScrollSpeed = 150;
        public float VerticalTileSize = 1024;

        private Vector3 _startPosition;

        private void Start()
        {
            Contract.Requires(
                ScrollSpeed >= 0,
                $"{nameof(ScrollSpeed)} was not set to a non-negative value on '{gameObject}.{this}'.");
            Contract.Requires(
                VerticalTileSize > 0,
                $"{nameof(VerticalTileSize)} was not set to a positive value on '{gameObject}.{this}'.");

            _startPosition = transform.position;
        }

        private void Update()
        {
            float newPosition = Mathf.Repeat(Time.time * ScrollSpeed, VerticalTileSize);
            transform.position = _startPosition + Vector3.down * newPosition;
        }
    }
}