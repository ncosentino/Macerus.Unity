using System.Linq;

using Assets.Scripts.Unity.Threading;

using NexusLabs.Contracts;

using ProjectXyz.Plugins.Features.Combat.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors
{
    public sealed class CantBePushedBehaviour :
        MonoBehaviour,
        ICantBePushedBehaviour
    {
        public Rigidbody2D RigidBody { get; set; }

        public ICombatTurnManager CombatTurnManager { get; set; }

        public IDispatcher Dispatcher { get; set; }

        private void Start()
        {
            this.RequiresNotNull(RigidBody, nameof(RigidBody));
            this.RequiresNotNull(CombatTurnManager, nameof(CombatTurnManager));
            this.RequiresNotNull(Dispatcher, nameof(Dispatcher));

            CombatTurnManager.CombatStarted += CombatTurnManager_CombatStarted;
            CombatTurnManager.TurnProgressed += CombatTurnManager_TurnProgressed;
            CombatTurnManager.CombatEnded += CombatTurnManager_CombatEnded;
        }

        private void OnDestroy()
        {
            if (CombatTurnManager != null)
            {
                CombatTurnManager.CombatStarted -= CombatTurnManager_CombatStarted;
                CombatTurnManager.TurnProgressed -= CombatTurnManager_TurnProgressed;
                CombatTurnManager.CombatEnded -= CombatTurnManager_CombatEnded;
            }
        }

        private void CombatTurnManager_CombatEnded(object sender, CombatEndedEventArgs e) => Dispatcher.RunOnMainThread(() =>
        {
            RigidBody.velocity = Vector2.zero;
            RigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        });

        private void CombatTurnManager_TurnProgressed(object sender, TurnProgressedEventArgs e) => Dispatcher.RunOnMainThread(() =>
        {
            RigidBody.velocity = Vector2.zero;
            RigidBody.constraints = e.ActorWithNextTurn == gameObject.GetGameObject()
                ? RigidbodyConstraints2D.FreezeRotation
                : RigidbodyConstraints2D.FreezeAll;
        });

        private void CombatTurnManager_CombatStarted(object sender, CombatStartedEventArgs e) => Dispatcher.RunOnMainThread(() =>
        {
            RigidBody.velocity = Vector2.zero;
            RigidBody.constraints = e.ActorOrder.First() == gameObject.GetGameObject()
                ? RigidbodyConstraints2D.FreezeRotation
                : RigidbodyConstraints2D.FreezeAll;
        });
    }
}
