using System.Collections.Generic;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment.Api
{
    public interface IEquipmentSlotsFactory
    {
        IEnumerable<IEquipSlotPrefab> CreateEquipmentSlots(
            IHasEquipmentBehavior hasEquipmentBehavior,
            ICanEquipBehavior canEquipBehavior);
    }
}