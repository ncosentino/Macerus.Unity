using Assets.Scripts.Unity.Resources;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment
{
    public sealed class EquippableEquipSlotItemBehaviourStitcher
    {
        public IReadOnlyEquippableEquipSlotItemBehaviour Attach(IEquipSlotPrefab equipSlot)
        {
            var equippableEquipSlotItemBehaviour = equipSlot.AddComponent<EquippableEquipSlotItemBehaviour>();
            // TODO: stitch the things
            return equippableEquipSlotItemBehaviour;
        }
    }

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