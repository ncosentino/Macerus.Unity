using System.Linq;

using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects;

using UnityEngine;
using Macerus.Plugins.Features.Encounters.Triggers;

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
                .Get<IReadOnlyEncounterTriggerPropertiesBehavior>()
                .SingleOrDefault();
            if (properties == null)
            {
                return;
            }

            _encounterTriggerBehaviourStitcher.Stitch(
                unityGameObject,
                properties);
        }
    }
}
