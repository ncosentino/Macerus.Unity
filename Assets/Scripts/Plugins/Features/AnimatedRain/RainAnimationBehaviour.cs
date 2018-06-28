using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedRain
{
    public sealed class RainAnimationBehaviour : MonoBehaviour
    {
        public float ScrollSpeed;
        public float VerticalTileSize;

        private Vector3 startPosition;

        private void Start()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            float newPosition = Mathf.Repeat(Time.time * ScrollSpeed, VerticalTileSize);
            transform.position = startPosition + Vector3.down * newPosition;
        }
    }
}