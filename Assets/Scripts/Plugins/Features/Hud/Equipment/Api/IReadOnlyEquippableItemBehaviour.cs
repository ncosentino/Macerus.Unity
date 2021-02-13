﻿using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment.Api
{
    public interface IReadOnlyEquippableItemBehaviour
    {
        ICanBeEquippedBehavior CanBeEquippedBehavior { get; }

        bool CanPrepareForEquipping(
            ICanEquipBehavior canEquipBehavior,
            IIdentifier targetEquipSlotId);
    }
}