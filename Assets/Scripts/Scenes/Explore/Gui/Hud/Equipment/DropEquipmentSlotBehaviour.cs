using System.Linq;
using Assets.Scripts.Scenes.Explore.GameObjects;
using Assets.Scripts.Unity.GameObjects;
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

        //public void OnDrop(PointerEventData eventData)
        //{
        //    var hasGameObjectBehaviour = eventData
        //        .pointerDrag
        //        .GetComponent<IHasGameObject>();
        //    if (hasGameObjectBehaviour == null)
        //    {
        //        Debug.Log($"'{eventData.pointerDrag}' does not have an IHasGameObject component.");
        //        return;
        //    }

        //    var canBeEquippedBehavior = hasGameObjectBehaviour
        //        .GameObject
        //        .Get<ICanBeEquippedBehavior>()
        //        .SingleOrDefault();
        //    if (canBeEquippedBehavior == null)
        //    {
        //        return;
        //    }

        //    // if there's an source item collection we:
        //    // - check to see if we can even equip this thing
        //    // - if we can, we remove the item from the source
        //    // - then we equip the item
        //    // FIXME: this logic does *NOT* handle the situation where we
        //    // cannot equip something once it's removed from the source:
        //    // i.e. diablo 2 style "stats while in inventory" enchanment types
        //    var sourceItemContainerBehaviour = eventData
        //        .pointerDrag
        //        .GetComponent<ISourceItemContainerBehaviour>();
        //    if (sourceItemContainerBehaviour != null)
        //    {
        //        if (!CanEquipBehavior.CanEquip(
        //            TargetEquipSlotId,
        //            canBeEquippedBehavior))
        //        {
        //            return;
        //        }

        //        if (!sourceItemContainerBehaviour
        //            .SourceItemContainer
        //            .TryRemoveItem((IGameObject)canBeEquippedBehavior.Owner)) // FIXME: barf at this casting?
        //        {
        //            return;
        //        }
        //    }

        //    var equipResult = CanEquipBehavior.TryEquip(
        //        TargetEquipSlotId,
        //        canBeEquippedBehavior);
        //    if (equipResult)
        //    {
        //        gameObject.RemoveComponents<IHasGameObject>();
        //        gameObject
        //            .AddComponent<HasGameObjectBehaviour>()
        //            .GameObject = (IGameObject)canBeEquippedBehavior.Owner; // FIXME: barf at this casting?
        //    }

        //    Debug.Log($"Equipped: {equipResult}");
        //}

        public void OnDrop(PointerEventData eventData)
        {
            var equippableItemBehaviour = eventData
                .pointerDrag
                .GetComponent<IReadOnlyEquippableItemBehaviour>();
            if (equippableItemBehaviour == null)
            {
                return;
            }

            var item = eventData
                .pointerDrag
                .GetComponent<IReadOnlyHasGameObject>()
                ?.GameObject;
            Contract.RequiresNotNull(
                item,
                $"'{equippableItemBehaviour}' does not have " +
                $"'{nameof(IReadOnlyHasGameObject)}' as a sibling component " +
                $"with a game object set.");

            var canBeEquippedBehavior = equippableItemBehaviour.CanBeEquippedBehavior;
            Contract.RequiresNotNull(
                canBeEquippedBehavior,
                $"'{equippableItemBehaviour}' does not have " +
                $"'{nameof(equippableItemBehaviour.CanBeEquippedBehavior)}' set.");

            if (!CanEquipBehavior.CanEquip(
                TargetEquipSlotId,
                canBeEquippedBehavior))
            {
                return;
            }

            if (!equippableItemBehaviour.CanPrepareForEquipping(
                CanEquipBehavior,
                TargetEquipSlotId))
            {
                return;
            }

            var equipResult = CanEquipBehavior.TryEquip(
                TargetEquipSlotId,
                canBeEquippedBehavior);
            if (equipResult)
            {
                gameObject.RemoveComponents<IHasGameObject>();
                gameObject
                    .AddComponent<HasGameObjectBehaviour>()
                    .GameObject = item;
            }

            Debug.Log($"Equipped: {equipResult}");
        }
    }
}
