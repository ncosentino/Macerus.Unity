using ProjectXyz.Plugins.Features.Combat.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors
{
    public interface ICantBePushedBehaviour
    {
        Rigidbody2D RigidBody { get; set; }

        ICombatTurnManager CombatTurnManager { get; set; }
    }
}
