using System.Collections.Generic;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public interface IEquipmentSlotsFactory
    {
        IEnumerable<GameObject> CreateEquipmentSlots(
            IHasEquipmentBehavior hasEquipmentBehavior,
            ICanEquipBehavior canEquipBehavior);
    }
}