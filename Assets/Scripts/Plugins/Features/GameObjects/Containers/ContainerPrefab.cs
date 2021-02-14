using System;
using System.Linq;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerPrefab : IContainerPrefab
    {
        private readonly Lazy<SpriteRenderer> _spriteRenderer;
        private readonly Lazy<Collider2D> _collision;
        private readonly Lazy<Collider2D> _trigger;

        public ContainerPrefab(GameObject gameObject)
        {
            GameObject = gameObject;
            _spriteRenderer = new Lazy<SpriteRenderer>(() => gameObject
                .GetComponent<SpriteRenderer>());
            _collision = new Lazy<Collider2D>(() => gameObject
                .GetComponents<Collider2D>()
                .First(x => !x.isTrigger));
            _trigger = new Lazy<Collider2D>(() => gameObject
                .GetComponents<Collider2D>()
                .First(x => x.isTrigger));
        }

        public GameObject GameObject { get; }

        public SpriteRenderer SpriteRenderer => _spriteRenderer.Value;

        public Collider2D Collision => _collision.Value;

        public Collider2D Trigger => _trigger.Value;
    }
}
