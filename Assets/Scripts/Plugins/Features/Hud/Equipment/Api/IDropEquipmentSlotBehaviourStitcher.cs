using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment.Api
{
    public interface IDropEquipmentSlotBehaviourStitcher
    {
        IReadOnlyDropEquipmentSlotBehaviour Attach(
            IEquipSlotPrefab equipSlotGameObject,
            IIdentifier targetEquipSlotId,
            ICanEquipBehavior canEquipBehavior);
    }
}