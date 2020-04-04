using System.Collections.Generic;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public interface IEquipmentSlotsFactory
    {
        IEnumerable<IEquipSlotPrefab> CreateEquipmentSlots(
            IHasEquipmentBehavior hasEquipmentBehavior,
            ICanEquipBehavior canEquipBehavior);
    }
}