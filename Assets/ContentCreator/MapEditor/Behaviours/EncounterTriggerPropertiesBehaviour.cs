
using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class EncounterTriggerPropertiesBehaviour :
        MonoBehaviour,
        IConvertableBehaviour
    {
        public bool MustBeMoving;

        public string EncounterId;

        public double IntervalInMilliseconds;

        public double EncounterChance;
    }
}
