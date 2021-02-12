using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment
{
    public sealed class EquippableEquipSlotItemBehaviour :
        MonoBehaviour,
        IEquippableEquipSlotItemBehaviour
    {
        public ICanBeEquippedBehavior CanBeEquippedBehavior { get; set; }

        public ICanEquipBehavior CanEquipBehavior { get; set; }

        public IIdentifier EquipSlotId { get; set; }

        public bool CanPrepareForEquipping(
            ICanEquipBehavior canEquipBehavior,
            IIdentifier targetEquipSlotId)
        {
            // no op!
            if (CanEquipBehavior == canEquipBehavior &&
                EquipSlotId.Equals(targetEquipSlotId))
            {
                return false;
            }

            // try to unequip it from the current slot
            ICanBeEquippedBehavior unequipped;
            if (!CanEquipBehavior.TryUnequip(
                EquipSlotId,
                out unequipped))
            {
                return false;
            }

            return true;
        }
    }
}