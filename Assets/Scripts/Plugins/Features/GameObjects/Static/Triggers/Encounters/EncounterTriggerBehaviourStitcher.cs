using System;

using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.Encounters.Triggers;

using NexusLabs.Framework;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static.Triggers
{
    public sealed class EncounterTriggerBehaviourStitcher : IEncounterTriggerBehaviourStitcher
    {
        private readonly IRandom _random;
        private readonly IEncounterManager _encounterManager;
        private readonly IFilterContextProvider _filtercontextProvider;

        public EncounterTriggerBehaviourStitcher(
            IRandom random,
            IEncounterManager encounterManager,
            IFilterContextProvider filtercontextProvider)
        {
            _random = random;
            _encounterManager = encounterManager;
            _filtercontextProvider = filtercontextProvider;
        }

        public void Stitch(
            GameObject unityGameObject,
            IReadOnlyEncounterTriggerPropertiesBehavior encounterTriggerPropertiesBehavior)
        {
            var encounterTriggerBehaviour = unityGameObject.AddComponent<EncounterTriggerBehaviour>();

            var encounterTriggerHandler = new EncounterTriggerHandler(
                _random,
                encounterTriggerBehaviour,
                encounterTriggerPropertiesBehavior.MustBeMoving,
                encounterTriggerPropertiesBehavior.EncounterChance,
                TimeSpan.FromMilliseconds( // FIXME: this casting is horrendous
                    ((IInterval<double>)encounterTriggerPropertiesBehavior.EncounterInterval).Value));
            
            encounterTriggerHandler.Encounter += (s, e) =>
            {
                var context = _filtercontextProvider.GetContext();
                _encounterManager.StartEncounter(
                    context,
                    encounterTriggerPropertiesBehavior.EncounterId);
            };
        }
    }
}
