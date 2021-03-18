using System;

using Macerus.Plugins.Features.Encounters.Triggers;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static.Triggers
{
    public sealed class EncounterTriggerBehaviour : 
        MonoBehaviour,
        IObservableEncounterTrigger
    {
        public event EventHandler<GameObjectTriggerEventArgs> TriggerStay;

        public event EventHandler<GameObjectTriggerEventArgs> TriggerEnter;

        public event EventHandler<GameObjectTriggerEventArgs> TriggerExit;

        private void OnTriggerStay2D(Collider2D collision)
        {
            var gameObject = collision.gameObject.GetGameObject();
            if (gameObject == null)
            {
                return;
            }

            TriggerStay?.Invoke(this, new GameObjectTriggerEventArgs(gameObject));
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var gameObject = collision.gameObject.GetGameObject();
            if (gameObject == null)
            {
                return;
            }

            TriggerEnter?.Invoke(this, new GameObjectTriggerEventArgs(gameObject));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var gameObject = collision.gameObject.GetGameObject();
            if (gameObject == null)
            {
                return;
            }

            TriggerExit?.Invoke(this, new GameObjectTriggerEventArgs(gameObject));
        }
    }
}
