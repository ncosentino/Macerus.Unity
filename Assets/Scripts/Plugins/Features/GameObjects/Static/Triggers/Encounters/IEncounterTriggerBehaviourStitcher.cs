using ProjectXyz.Api.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static.Triggers
{
    public interface IEncounterTriggerBehaviourStitcher
    {
        void Stitch(
            GameObject unityGameObject,
            IIdentifier encounterId,
            double encounterChance,
            IInterval encounterInterval,
            bool mustBeMoving);
    }
}
