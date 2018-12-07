﻿using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment;
using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Framework.Contracts;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public sealed class HasGuiEquipmentBehaviour :
        MonoBehaviour,
        IHasGuiEquipmentBehaviour
    {
        private readonly List<GameObject> _equipmentSlots;

        public HasGuiEquipmentBehaviour()
        {
            _equipmentSlots = new List<GameObject>();
        }

        public IEquipmentSlotsFactory EquipmentSlotsFactory { get; set; }

        public IObjectDestroyer ObjectDestroyer { get; set; }

        public IGameObjectManager GameObjectManager { get; set; }

        public IHasEquipmentBehavior HasEquipmentBehavior { get; set; }

        public ICanEquipBehavior CanEquipBehavior { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                EquipmentSlotsFactory,
                $"{nameof(EquipmentSlotsFactory)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                ObjectDestroyer,
                $"{nameof(ObjectDestroyer)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                GameObjectManager,
                $"{nameof(GameObjectManager)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                HasEquipmentBehavior,
                $"{nameof(HasEquipmentBehavior)} was not set on '{HasEquipmentBehavior}.{this}'.");
            // NOTE: can be null :)
            //Contract.RequiresNotNull(
            //    CanEquipBehavior,
            //    $"{nameof(CanEquipBehavior)} was not set on '{CanEquipBehavior}.{this}'.");

            // FIXME: find a better way to associate the thing in the UI
            var inventoryEquipmentUi = GameObjectManager
                .FindAll(x => x.name == "Equipment")
                .First();

            var equipSlotObjects = EquipmentSlotsFactory.CreateEquipmentSlots(HasEquipmentBehavior.SupportedEquipSlotIds);
            foreach (var equipmentSlot in equipSlotObjects)
            {
                equipmentSlot.transform.SetParent(
                    inventoryEquipmentUi.transform,
                    false);
                _equipmentSlots.Add(equipmentSlot);
            }
        }

        private void OnDestroy()
        {
            foreach (var equipmentSlot in _equipmentSlots)
            {
                ObjectDestroyer.Destroy(equipmentSlot);
            }
            
            _equipmentSlots.Clear();
        }
    }
}