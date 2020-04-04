using System;
using Assets.Scripts.Scenes.Explore.GameObjects;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public sealed class EquippableInventoryItemBehaviour :
        MonoBehaviour,
        IEquippableInventoryItemBehaviour
    {
        public ICanBeEquippedBehavior CanBeEquippedBehavior { get; set; }

        public Func<IItemContainerBehavior> SourceItemContainerCallback { get; set; }

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
            if (!SourceItemContainerCallback().TryRemoveItem((IGameObject)CanBeEquippedBehavior.Owner)) // FIXME: barf at this casting?
            {
                return false;
            }

            return true;
        }
    }
}