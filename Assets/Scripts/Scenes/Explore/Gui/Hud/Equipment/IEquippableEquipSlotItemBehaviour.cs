﻿using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public interface IEquippableEquipSlotItemBehaviour : IReadOnlyEquippableEquipSlotItemBehaviour
    {
        new ICanEquipBehavior CanEquipBehavior { get; set; }

        new IIdentifier EquipSlotId { get; set; }
    }
}