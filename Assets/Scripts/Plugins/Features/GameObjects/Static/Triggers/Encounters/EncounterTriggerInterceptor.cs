using System;
using System.Globalization;
using System.Linq;

using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using Macerus.Plugins.Features.GameObjects.Static.Api;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static.Triggers
{
    public sealed class EncounterTriggerInterceptor : IDiscoverableGameObjectBehaviorInterceptor
    {
        private readonly IEncounterTriggerBehaviourStitcher _encounterTriggerBehaviourStitcher;

        public EncounterTriggerInterceptor(IEncounterTriggerBehaviourStitcher encounterTriggerBehaviourStitcher)
        {
            _encounterTriggerBehaviourStitcher = encounterTriggerBehaviourStitcher;
        }

        public void Intercept(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var properties = gameObject
                .Get<IReadOnlyStaticGameObjectPropertiesBehavior>()
                .SingleOrDefault()
                ?.Properties;
            if (properties == null)
            {
                return;
            }

            if (!properties.TryGetValue(
                "EncounterId",
                out var rawEncounterId))
            {
                return;
            }

            var encounterId = new StringIdentifier(Convert.ToString(
                rawEncounterId,
                CultureInfo.InvariantCulture));

            if (!properties.TryGetValue(
                "EncounterChance",
                out var rawEncounterChance))
            {
                return;
            }

            if (!double.TryParse(
                Convert.ToString(rawEncounterChance, CultureInfo.InvariantCulture),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var encounterChance))
            {
                throw new InvalidProgramException(
                    $"Could not parse encounter chance '{rawEncounterChance}'.");
            }

            if (!properties.TryGetValue(
                "EncounterInterval",
                out var rawEncounterInterval))
            {
                return;
            }

            if (!double.TryParse(
                Convert.ToString(rawEncounterInterval, CultureInfo.InvariantCulture),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var encounterInterval))
            {
                throw new InvalidProgramException(
                    $"Could not parse encounter interval '{encounterInterval}'.");
            }

            bool mustBeMoving = false;
            if (properties.TryGetValue(
                "MustBeMoving",
                out var rawMustBeMoving))
            {
                mustBeMoving = Convert.ToBoolean(rawMustBeMoving);
            }

            _encounterTriggerBehaviourStitcher.Stitch(
                unityGameObject,
                encounterId,
                encounterChance,
                new Interval<double>(encounterInterval),
                mustBeMoving);
        }
    }
}
