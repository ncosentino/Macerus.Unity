using System;
using System.Linq;

using Assets.Scripts.Plugins.Features.Hud.Api;
using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;
using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment
{
    public sealed class DragEquipmentItemBehaviourStitcher : IDragEquipmentItemBehaviourStitcher
    {
        private readonly IDragItemFactory _dragItemFactory;
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly IDropItemHandler _dropItemHandler;
        private readonly Lazy<GameObject> _lazyInventoryUiGameObject;

        public DragEquipmentItemBehaviourStitcher(
            IDragItemFactory dragItemFactory,
            IObjectDestroyer objectDestroyer,
            IGameObjectManager gameObjectManager,
            IDropItemHandler dropItemHandler)
        {
            _dragItemFactory = dragItemFactory;
            _objectDestroyer = objectDestroyer;
            _lazyInventoryUiGameObject = new Lazy<GameObject>(() =>
            {
                return gameObjectManager
                    .FindAll(x => x.name == "Inventory")
                    .First();
            });
            _dropItemHandler = dropItemHandler;
        }

        private GameObject InventoryUiGameObject => _lazyInventoryUiGameObject.Value;

        public IReadOnlyDragEquipmentItemBehaviour Attach(IEquipSlotPrefab equipSlot)
        {
            var dragInventoryListItemBehaviour = equipSlot.AddComponent<DragEquipmentItemBehaviour>();
            dragInventoryListItemBehaviour.DragItemFactory = _dragItemFactory;
            dragInventoryListItemBehaviour.ObjectDestroyer = _objectDestroyer;
            dragInventoryListItemBehaviour.DropItemHandler = _dropItemHandler;
            dragInventoryListItemBehaviour.InventoryGameObject = InventoryUiGameObject;
            dragInventoryListItemBehaviour.EquipSlot = equipSlot;

            return dragInventoryListItemBehaviour;
        }
    }
}