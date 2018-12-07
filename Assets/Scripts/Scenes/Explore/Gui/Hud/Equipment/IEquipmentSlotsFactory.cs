using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public interface IEquipmentSlotsFactory
    {
        IEnumerable<GameObject> CreateEquipmentSlots(IEnumerable<IIdentifier> equipSlotIds);
    }
}