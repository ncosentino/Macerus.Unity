using Macerus.Plugins.Features.Encounters.GamObjects.Static.Triggers;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static.Triggers
{
    public interface IEncounterTriggerBehaviourStitcher
    {
        void Stitch(
            GameObject unityGameObject,
            IReadOnlyEncounterTriggerPropertiesBehavior encounterTriggerPropertiesBehavior);
    }
}
