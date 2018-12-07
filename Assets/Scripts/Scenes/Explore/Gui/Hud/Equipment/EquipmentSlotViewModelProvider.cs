using System.Collections.Generic;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public sealed class EquipmentSlotViewModelProvider
    {
        //// TODO: can we please load these slots from a data source somewhere?
        public IEnumerable<IEquipmentSlotViewModel> GetViewModels()
        {
            const string LOL_RESOURCE = @"Graphics/Gui/Inventory/empty hand slot";

            yield return new EquipmentSlotViewModel("Gui/Prefabs/Inventory/EquipmentSlot",     LOL_RESOURCE, 92 + 80,   92 + 64); // amulet
            yield return new EquipmentSlotViewModel("Gui/Prefabs/Inventory/EquipmentSlot",     LOL_RESOURCE, 92 + 80,   92 + 32); // ring1
            yield return new EquipmentSlotViewModel("Gui/Prefabs/Inventory/EquipmentSlot",     LOL_RESOURCE, 92 + 80,   92 + 0); // ring2
            yield return new EquipmentSlotViewModel("Gui/Prefabs/Inventory/EquipmentSlot",     LOL_RESOURCE, 92 + 0,    92 + 80); // helm
            yield return new EquipmentSlotViewModel("Gui/Prefabs/Inventory/TallEquipmentSlot", LOL_RESOURCE, 92 + 0,    76 + 0); // body
            yield return new EquipmentSlotViewModel("Gui/Prefabs/Inventory/TallEquipmentSlot", LOL_RESOURCE, 92 + -32,  76 + 0); // cloak
            yield return new EquipmentSlotViewModel("Gui/Prefabs/Inventory/TallEquipmentSlot", LOL_RESOURCE, 92 + -32,  76 + 64); // left hand
            yield return new EquipmentSlotViewModel("Gui/Prefabs/Inventory/TallEquipmentSlot", LOL_RESOURCE, 92 + 32,   76 + 64); // right hand
            yield return new EquipmentSlotViewModel("Gui/Prefabs/Inventory/TallEquipmentSlot", LOL_RESOURCE, 92 + -32,  76 + -64); // pants
            yield return new EquipmentSlotViewModel("Gui/Prefabs/Inventory/EquipmentSlot",     LOL_RESOURCE, 92 + 32,   92 + 16); // shoulders
            yield return new EquipmentSlotViewModel("Gui/Prefabs/Inventory/EquipmentSlot",     LOL_RESOURCE, 92 + 32,   92 + -16); // gloves
            yield return new EquipmentSlotViewModel("Gui/Prefabs/Inventory/EquipmentSlot",     LOL_RESOURCE, 92 + 32,   92 + -48); // belt
            yield return new EquipmentSlotViewModel("Gui/Prefabs/Inventory/EquipmentSlot",     LOL_RESOURCE, 92 + 32,   92 + -80); // boots
        }
    }
}