using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public interface IIconEquipmentSlotBehaviourStitcher
    {
        IReadOnlyIconEquipmentSlotBehaviour Attach(
            GameObject equipSlotGameObject,
            IIdentifier targetEquipSlotId,
            ICanEquipBehavior canEquipBehavior,
            string emptyIconResource);
    }
}