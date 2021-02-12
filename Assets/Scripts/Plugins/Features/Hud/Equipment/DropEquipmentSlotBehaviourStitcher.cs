using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;
using Assets.Scripts.Unity.Resources;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment
{
    public class DropEquipmentSlotBehaviourStitcher : IDropEquipmentSlotBehaviourStitcher
    {
        public IReadOnlyDropEquipmentSlotBehaviour Attach(
            IEquipSlotPrefab equipSlotGameObject,
            IIdentifier targetEquipSlotId,
            ICanEquipBehavior canEquipBehavior)
        {
            var dropEquipmentSlotBehaviour = equipSlotGameObject.AddComponent<DropEquipmentSlotBehaviour>();

            dropEquipmentSlotBehaviour.TargetEquipSlotId = targetEquipSlotId;
            dropEquipmentSlotBehaviour.CanEquipBehavior = canEquipBehavior;

            return dropEquipmentSlotBehaviour;
        }
    }
}