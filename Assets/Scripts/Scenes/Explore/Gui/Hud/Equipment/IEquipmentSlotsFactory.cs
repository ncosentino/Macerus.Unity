using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public interface IEquipmentSlotsFactory
    {
        IEnumerable<GameObject> CreateEquipmentSlots();
    }
}