using System;

using Assets.Scripts.Unity;
using Assets.Scripts.Unity.GameObjects;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Behaviours.Generic
{
    public sealed class SelfDestructBehaviour : MonoBehaviour
    {
        private float _triggerTime;

        public ITimeProvider TimeProvider { get; set; }

        public IObjectDestroyer ObjectDestroyer { get; set; }

        public TimeSpan Duration { get; set; }

        private void Start()
        {
            this.RequiresNotNull(TimeProvider, nameof(TimeProvider));
            this.RequiresNotNull(ObjectDestroyer, nameof(ObjectDestroyer));

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
