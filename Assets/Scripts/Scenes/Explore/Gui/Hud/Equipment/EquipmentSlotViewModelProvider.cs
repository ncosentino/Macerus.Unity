using System.Collections.Generic;
using ProjectXyz.Shared.Framework;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public sealed class EquipmentSlotViewModelProvider
    {
        //// TODO: can we please load these slots from a data source somewhere?
        public IEnumerable<IEquipmentSlotViewModel> GetViewModels()
        {
            const string WIP_PLACEHOLDER_RESOURCE = @"Graphics/Gui/Inventory/empty hand slot";

            yield return new EquipmentSlotViewModel(new StringIdentifier("amulet"),     "Gui/Prefabs/Inventory/EquipmentSlot",     WIP_PLACEHOLDER_RESOURCE, 92 + 80,   92 + 64);
            yield return new EquipmentSlotViewModel(new StringIdentifier("ring1"),      "Gui/Prefabs/Inventory/EquipmentSlot",     WIP_PLACEHOLDER_RESOURCE, 92 + 80,   92 + 32);
            yield return new EquipmentSlotViewModel(new StringIdentifier("ring2"),      "Gui/Prefabs/Inventory/EquipmentSlot",     WIP_PLACEHOLDER_RESOURCE, 92 + 80,   92 + 0);
            yield return new EquipmentSlotViewModel(new StringIdentifier("head"),       "Gui/Prefabs/Inventory/EquipmentSlot",     WIP_PLACEHOLDER_RESOURCE, 92 + 0,    92 + 80);
            yield return new EquipmentSlotViewModel(new StringIdentifier("body"),       "Gui/Prefabs/Inventory/TallEquipmentSlot", WIP_PLACEHOLDER_RESOURCE, 92 + 0,    76 + 0);
            yield return new EquipmentSlotViewModel(new StringIdentifier("back"),      "Gui/Prefabs/Inventory/TallEquipmentSlot", WIP_PLACEHOLDER_RESOURCE, 92 + -32,  76 + 0);
            yield return new EquipmentSlotViewModel(new StringIdentifier("left hand"),  "Gui/Prefabs/Inventory/TallEquipmentSlot", WIP_PLACEHOLDER_RESOURCE, 92 + -32,  76 + 64);
            yield return new EquipmentSlotViewModel(new StringIdentifier("right hand"), "Gui/Prefabs/Inventory/TallEquipmentSlot", WIP_PLACEHOLDER_RESOURCE, 92 + 32,   76 + 64);
            yield return new EquipmentSlotViewModel(new StringIdentifier("legs"),       "Gui/Prefabs/Inventory/TallEquipmentSlot", WIP_PLACEHOLDER_RESOURCE, 92 + -32,  76 + -64);
            yield return new EquipmentSlotViewModel(new StringIdentifier("shoulders"),  "Gui/Prefabs/Inventory/EquipmentSlot",     WIP_PLACEHOLDER_RESOURCE, 92 + 32,   92 + 16);
            yield return new EquipmentSlotViewModel(new StringIdentifier("hands"),     "Gui/Prefabs/Inventory/EquipmentSlot",     WIP_PLACEHOLDER_RESOURCE, 92 + 32,   92 + -16);
            yield return new EquipmentSlotViewModel(new StringIdentifier("waist"),       "Gui/Prefabs/Inventory/EquipmentSlot",     WIP_PLACEHOLDER_RESOURCE, 92 + 32,   92 + -48);
            yield return new EquipmentSlotViewModel(new StringIdentifier("feet"),      "Gui/Prefabs/Inventory/EquipmentSlot",     WIP_PLACEHOLDER_RESOURCE, 92 + 32,   92 + -80);
        }
    }
}