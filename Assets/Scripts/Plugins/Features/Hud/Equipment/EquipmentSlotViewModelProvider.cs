using System.Collections.Generic;

using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;

using ProjectXyz.Shared.Framework;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment
{
    public sealed class EquipmentSlotViewModelProvider
    {
        //// TODO: can we please load these slots from a data source somewhere?
        public IEnumerable<IEquipmentSlotViewModel> GetViewModels()
        {
            const string WIP_PLACEHOLDER_RESOURCE = @"Graphics/Gui/Inventory/empty inventory slot";
            const string EMPTY_AMULET = @"Graphics/Gui/Inventory/empty amulet slot";
            const string EMPTY_BACK = @"Graphics/Gui/Inventory/empty cloak slot";
            const string EMPTY_BELT = @"Graphics/Gui/Inventory/empty belt slot";
            const string EMPTY_BODY = @"Graphics/Gui/Inventory/empty body slot";
            const string EMPTY_BOOTS = @"Graphics/Gui/Inventory/empty boots slot";
            const string EMPTY_GLOVES = @"Graphics/Gui/Inventory/empty gloves slot";
            const string EMPTY_HANDS = @"Graphics/Gui/Inventory/empty hand slot";
            const string EMPTY_HEAD = @"Graphics/Gui/Inventory/empty helm slot";
            const string EMPTY_RING = @"Graphics/Gui/Inventory/empty ring slot";

            yield return new EquipmentSlotViewModel(new StringIdentifier("amulet"),     "Gui/Prefabs/Inventory/EquipmentSlot",     EMPTY_AMULET, 92 + 80,   92 + 64);
            yield return new EquipmentSlotViewModel(new StringIdentifier("ring1"),      "Gui/Prefabs/Inventory/EquipmentSlot",     EMPTY_RING, 92 + 80,   92 + 32);
            yield return new EquipmentSlotViewModel(new StringIdentifier("ring2"),      "Gui/Prefabs/Inventory/EquipmentSlot",     EMPTY_RING, 92 + 80,   92 + 0);
            yield return new EquipmentSlotViewModel(new StringIdentifier("head"),       "Gui/Prefabs/Inventory/EquipmentSlot",     EMPTY_HEAD, 92 + 0,    92 + 80);
            yield return new EquipmentSlotViewModel(new StringIdentifier("body"),       "Gui/Prefabs/Inventory/TallEquipmentSlot", EMPTY_BODY, 92 + 0,    76 + 0);
            yield return new EquipmentSlotViewModel(new StringIdentifier("back"),      "Gui/Prefabs/Inventory/TallEquipmentSlot",  EMPTY_BACK, 92 + -32,  76 + 0);
            yield return new EquipmentSlotViewModel(new StringIdentifier("left hand"),  "Gui/Prefabs/Inventory/TallEquipmentSlot", EMPTY_HANDS, 92 + -32,  76 + 64);
            yield return new EquipmentSlotViewModel(new StringIdentifier("right hand"), "Gui/Prefabs/Inventory/TallEquipmentSlot", EMPTY_HANDS, 92 + 32,   76 + 64);
            yield return new EquipmentSlotViewModel(new StringIdentifier("legs"),       "Gui/Prefabs/Inventory/TallEquipmentSlot", WIP_PLACEHOLDER_RESOURCE, 92 + -32,  76 + -64);
            yield return new EquipmentSlotViewModel(new StringIdentifier("shoulders"),  "Gui/Prefabs/Inventory/EquipmentSlot",     WIP_PLACEHOLDER_RESOURCE, 92 + 32,   92 + 16);
            yield return new EquipmentSlotViewModel(new StringIdentifier("hands"),     "Gui/Prefabs/Inventory/EquipmentSlot",      EMPTY_GLOVES, 92 + 32,   92 + -16);
            yield return new EquipmentSlotViewModel(new StringIdentifier("waist"),       "Gui/Prefabs/Inventory/EquipmentSlot",    EMPTY_BELT, 92 + 32,   92 + -48);
            yield return new EquipmentSlotViewModel(new StringIdentifier("feet"),      "Gui/Prefabs/Inventory/EquipmentSlot",      EMPTY_BOOTS, 92 + 32,   92 + -80);
        }
    }
}