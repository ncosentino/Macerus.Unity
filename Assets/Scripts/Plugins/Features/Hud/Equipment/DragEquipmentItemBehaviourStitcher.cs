using System;
using System.Linq;

using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;
using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;
using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment
{
    public sealed class DragEquipmentItemBehaviourStitcher : IDragEquipmentItemBehaviourStitcher
    {
        private readonly IDragItemFactory _dragItemFactory;
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly IMouseInput _mouseInput;
        private readonly IEquipmentItemDropUiFlow _equipmentItemDropUiFlow;
        private readonly Lazy<GameObject> _lazyInventoryUiGameObject;

        public DragEquipmentItemBehaviourStitcher(
            IDragItemFactory dragItemFactory,
            IObjectDestroyer objectDestroyer,
            IUnityGameObjectManager unityGameObjectManager,
            IMouseInput mouseInput,
            IEquipmentItemDropUiFlow equipmentItemDropUiFlow)
        {
            _dragItemFactory = dragItemFactory;
            _objectDestroyer = objectDestroyer;
            _lazyInventoryUiGameObject = new Lazy<GameObject>(() =>
            {
                return unityGameObjectManager
                    .FindAll(x => x.name == "Inventory")
                    .First();
            });
            _mouseInput = mouseInput;
            _equipmentItemDropUiFlow = equipmentItemDropUiFlow;
        }

        private GameObject InventoryUiGameObject => _lazyInventoryUiGameObject.Value;

        public IReadOnlyDragEquipmentItemBehaviour Attach(IEquipSlotPrefab equipSlot)
        {
            var dragEquipmentItemBehaviour = equipSlot.AddComponent<DragEquipmentItemBehaviour>();
            dragEquipmentItemBehaviour.DragItemFactory = _dragItemFactory;
            dragEquipmentItemBehaviour.ObjectDestroyer = _objectDestroyer;
            dragEquipmentItemBehaviour.MouseInput = _mouseInput;
            dragEquipmentItemBehaviour.InventoryGameObject = InventoryUiGameObject;
            dragEquipmentItemBehaviour.EquipSlot = equipSlot;
            dragEquipmentItemBehaviour.Dropped += (_, e) => _equipmentItemDropUiFlow.Execute(e);

            return dragEquipmentItemBehaviour;
        }
    }
}