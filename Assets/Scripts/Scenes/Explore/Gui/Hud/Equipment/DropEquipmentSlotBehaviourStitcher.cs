using System.Linq;
using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
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