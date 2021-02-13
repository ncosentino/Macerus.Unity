
using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class EquippableInventoryItemBehaviour :
        MonoBehaviour,
        IEquippableInventoryItemBehaviour
    {
        public ICanBeEquippedBehavior CanBeEquippedBehavior { get; set; }

        public IReadOnlyInventoryItemBehaviour InventoryItemBehaviour { get; set; }

        public void Start()
        {
            Contract.RequiresNotNull(
                CanBeEquippedBehavior,
                $"{nameof(CanBeEquippedBehavior)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                InventoryItemBehaviour,
                $"{nameof(InventoryItemBehaviour)} was not set on '{gameObject}.{this}'.");
        }

        public bool CanPrepareForEquipping(
            ICanEquipBehavior canEquipBehavior,
            IIdentifier targetEquipSlotId)
        {
            // if there's an source item collection we:
            // - check to see if we can even equip this thing
            // - if we can, we remove the item from the source
            // - then we equip the item
            // FIXME: this logic does *NOT* handle the situation where we
            // cannot equip something once it's removed from the source:
            // i.e. diablo 2 style "stats while in inventory" enchanment types
            if (!InventoryItemBehaviour.TryRemoveFromSourceContainer())
            {
                return false;
            }

            return true;
        }
    }
}