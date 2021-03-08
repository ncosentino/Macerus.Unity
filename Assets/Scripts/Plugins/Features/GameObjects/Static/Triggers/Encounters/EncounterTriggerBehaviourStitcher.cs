using System;

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
            IIdentifier encounterId,
            double encounterChance,
            IInterval encounterInterval,
            bool mustBeMoving)
        {
            var encounterTriggerBehaviour = unityGameObject.AddComponent<EncounterTriggerBehaviour>();
            encounterTriggerBehaviour.Random = _random;
            encounterTriggerBehaviour.ChanceToTrigger = encounterChance;
            encounterTriggerBehaviour.MustBeMoving = mustBeMoving;

            // FIXME: this casting is horrendous
            encounterTriggerBehaviour.TriggerInterval = TimeSpan.FromMilliseconds(
                ((IInterval<double>)encounterInterval).Value);
        }
    }
}
