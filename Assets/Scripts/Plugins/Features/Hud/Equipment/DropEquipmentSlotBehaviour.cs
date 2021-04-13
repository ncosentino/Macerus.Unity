using Assets.Scripts.Plugins.Features.GameObjects.Common;
using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;
using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Api.Framework;
using NexusLabs.Contracts;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment
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
                canBeEquippedBehavior,
                false))
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
                canBeEquippedBehavior,
                false);
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
