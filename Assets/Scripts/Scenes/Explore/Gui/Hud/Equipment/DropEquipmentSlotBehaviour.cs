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
    public interface IReadOnlyDropEquipmentSlotBehaviour
    {
        ICanEquipBehavior CanEquipBehavior { get; }

        IIdentifier TargetEquipSlotId { get; }
    }

    public class DropEquipmentSlotBehaviour :
        MonoBehaviour,
        IDropEquipmentSlotBehaviour,
        IDropHandler
    {
        public ICanEquipBehavior CanEquipBehavior { get; set; }

        public IIdentifier TargetEquipSlotId { get; set; }

        public void OnStart()
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

            Debug.Log(canBeEquippedBehavior);
            var equipResult = CanEquipBehavior.TryEquip(
                TargetEquipSlotId,
                canBeEquippedBehavior);
            Debug.Log($"Equipped: {equipResult}");
        }
    }

    public interface IDropEquipmentSlotBehaviourStitcher
    {
        IReadOnlyDropEquipmentSlotBehaviour Attach(
            GameObject equipSlotGameObject,
            IIdentifier targetEquipSlotId,
            ICanEquipBehavior canEquipBehavior);
    }

    public class DropEquipmentSlotBehaviourStitcher : IDropEquipmentSlotBehaviourStitcher
    {
        public IReadOnlyDropEquipmentSlotBehaviour Attach(
            GameObject equipSlotGameObject,
            IIdentifier targetEquipSlotId,
            ICanEquipBehavior canEquipBehavior)
        {
            var dropEquipmentSlotBehaviour = equipSlotGameObject
                .GetChildGameObjects()
                .Single(x => x.name == "Background")
                .AddComponent<DropEquipmentSlotBehaviour>();

            dropEquipmentSlotBehaviour.TargetEquipSlotId = targetEquipSlotId;
            dropEquipmentSlotBehaviour.CanEquipBehavior = canEquipBehavior;

            return dropEquipmentSlotBehaviour;
        }
    }
}
