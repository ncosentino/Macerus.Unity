using System.Linq;
using Assets.Scripts.Scenes.Explore.GameObjects;
using Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Framework.Contracts;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public class DropEquipmentSlotBehaviour :
        MonoBehaviour,
        IDropEquipmentSlotBehaviour,
        IDropHandler
    {
        public ICanEquipBehavior CanEquipBehavior { get; set; }

        public IIdentifier TargetEquipSlotId { get; set; }

        public void Start()
        {
            Contract.RequiresNotNull(
                CanEquipBehavior,
                $"{nameof(CanEquipBehavior)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                TargetEquipSlotId,
                $"{nameof(TargetEquipSlotId)} was not set on '{gameObject}.{this}'.");
        }

        public void OnDrop(PointerEventData eventData)
        {
            var hasGameObjectBehaviour = eventData
                .pointerDrag
                .GetComponent<IHasGameObject>();
            if (hasGameObjectBehaviour == null)
            {
                return;
            }

            var canBeEquippedBehavior = hasGameObjectBehaviour
                .GameObject
                .Get<ICanBeEquippedBehavior>()
                .SingleOrDefault();
            if (canBeEquippedBehavior == null)
            {
                return;
            }

            // if there's an source item collection we:
            // - check to see if we can even equip this thing
            // - if we can, we remove the item from the source
            // - then we equip the item
            // FIXME: this logic does *NOT* handle the situation where we
            // cannot equip something once it's removed from the source:
            // i.e. diablo 2 style "stats while in inventory" enchanment types
            var sourceItemContainerBehaviour = eventData
                .pointerDrag
                .GetComponent<ISourceItemContainerBehaviour>();
            if (sourceItemContainerBehaviour != null)
            {
                if (!CanEquipBehavior.CanEquip(
                    TargetEquipSlotId,
                    canBeEquippedBehavior))
                {
                    return;
                }

                if (!sourceItemContainerBehaviour
                    .SourceItemContainer
                    .TryRemoveItem((IGameObject)canBeEquippedBehavior.Owner)) // FIXME: barf at this casting?
                {
                    return;
                }
            }

            var equipResult = CanEquipBehavior.TryEquip(
                TargetEquipSlotId,
                canBeEquippedBehavior);
            Debug.Log($"Equipped: {equipResult}");
        }
    }
}
