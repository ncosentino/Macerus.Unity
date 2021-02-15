
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IHasGuiEquipmentBehaviourStitcher
    {
        IReadonlyHasGuiEquipmentBehaviour Attach(
            GameObject gameObject,
            IHasEquipmentBehavior hasEquipmentBehavior,
            ICanEquipBehavior canEquipBehavior);
    }
}