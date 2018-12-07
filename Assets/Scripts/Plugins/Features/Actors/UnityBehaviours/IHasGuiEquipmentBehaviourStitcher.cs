using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public interface IHasGuiEquipmentBehaviourStitcher
    {
        IReadonlyHasGuiEquipmentBehaviour Attach(
            GameObject gameObject,
            IHasEquipmentBehavior hasEquipmentBehavior,
            ICanEquipBehavior canEquipBehavior);
    }
}