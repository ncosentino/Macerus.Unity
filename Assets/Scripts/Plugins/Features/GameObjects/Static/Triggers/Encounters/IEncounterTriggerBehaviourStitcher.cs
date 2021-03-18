using Macerus.Plugins.Features.Encounters.Triggers.GamObjects.Static;

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
