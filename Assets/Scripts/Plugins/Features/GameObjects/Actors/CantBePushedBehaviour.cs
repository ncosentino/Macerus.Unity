using System.Linq;

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

        private void Start()
        {
            this.RequiresNotNull(RigidBody, nameof(RigidBody));
            this.RequiresNotNull(CombatTurnManager, nameof(CombatTurnManager));

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

        private void CombatTurnManager_CombatEnded(object sender, CombatEndedEventArgs e)
        {
            RigidBody.velocity = Vector2.zero;
            RigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        private void CombatTurnManager_TurnProgressed(object sender, TurnProgressedEventArgs e)
        {
            RigidBody.velocity = Vector2.zero;
            RigidBody.constraints = e.ActorWithNextTurn == gameObject.GetGameObject()
                ? RigidbodyConstraints2D.FreezeRotation
                : RigidbodyConstraints2D.FreezeAll;
        }

        private void CombatTurnManager_CombatStarted(object sender, CombatStartedEventArgs e)
        {
            RigidBody.velocity = Vector2.zero;
            RigidBody.constraints = e.ActorOrder.First() == gameObject.GetGameObject()
                ? RigidbodyConstraints2D.FreezeRotation
                : RigidbodyConstraints2D.FreezeAll;
        }
    }
}
