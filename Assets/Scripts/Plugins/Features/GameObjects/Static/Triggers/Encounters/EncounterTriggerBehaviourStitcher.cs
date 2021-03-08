using System;

using Macerus.Plugins.Features.Encounters.GamObjects.Static.Triggers;

using NexusLabs.Framework;

using ProjectXyz.Api.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static.Triggers
{
    public sealed class EncounterTriggerBehaviourStitcher : IEncounterTriggerBehaviourStitcher
    {
        private readonly IRandom _random;

        public EncounterTriggerBehaviourStitcher(IRandom random)
        {
            _random = random;
        }

        public void Stitch(
            GameObject unityGameObject,
            IReadOnlyEncounterTriggerPropertiesBehavior encounterTriggerPropertiesBehavior)
        {
            var encounterTriggerBehaviour = unityGameObject.AddComponent<EncounterTriggerBehaviour>();
            encounterTriggerBehaviour.Random = _random;
            encounterTriggerBehaviour.ChanceToTrigger = encounterTriggerPropertiesBehavior.EncounterChance;
            encounterTriggerBehaviour.MustBeMoving = encounterTriggerPropertiesBehavior.MustBeMoving;

            // FIXME: this casting is horrendous
            encounterTriggerBehaviour.TriggerInterval = TimeSpan.FromMilliseconds(
                ((IInterval<double>)encounterTriggerPropertiesBehavior.EncounterInterval).Value);
        }
    }
}
