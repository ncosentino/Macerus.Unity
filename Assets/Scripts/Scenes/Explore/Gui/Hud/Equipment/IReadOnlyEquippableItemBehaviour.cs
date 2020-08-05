﻿using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public interface IReadOnlyEquippableItemBehaviour
    {
        ICanBeEquippedBehavior CanBeEquippedBehavior { get; }

        bool CanPrepareForEquipping(
            ICanEquipBehavior canEquipBehavior,
            IIdentifier targetEquipSlotId);
    }
}