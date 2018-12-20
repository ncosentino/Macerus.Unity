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

            // TODO: need to remove from source if one exists... so do we do something like
            // source.TryRemove + destination.TryEquip?
            // is there a way to make that feel atomic?
            // do we try to equip it before removing it instead?

            var equipResult = CanEquipBehavior.TryEquip(
                TargetEquipSlotId,
                canBeEquippedBehavior);
            Debug.Log($"Equipped: {equipResult}");
        }
    }
}
