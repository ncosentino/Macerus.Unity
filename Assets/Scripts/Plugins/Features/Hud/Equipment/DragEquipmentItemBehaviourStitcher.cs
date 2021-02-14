using System;
using System.Linq;

using Assets.Scripts.Plugins.Features.Hud.Api;
using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;
using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;
using Assets.Scripts.Unity.Resources.Prefabs;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment
{
    public sealed class DragEquipmentItemBehaviourStitcher : IDragEquipmentItemBehaviourStitcher
    {
        private readonly IDragItemFactory _dragItemFactory;
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IDropItemHandler _dropItemHandler;
        private readonly IMouseInput _mouseInput;
        private readonly Lazy<GameObject> _lazyInventoryUiGameObject;

        public DragEquipmentItemBehaviourStitcher(
            IDragItemFactory dragItemFactory,
            IObjectDestroyer objectDestroyer,
            IUnityGameObjectManager unityGameObjectManager,
            IGameObjectManager gameObjectManager,
            IDropItemHandler dropItemHandler,
            IMouseInput mouseInput)
        {
            _dragItemFactory = dragItemFactory;
            _objectDestroyer = objectDestroyer;
            _gameObjectManager = gameObjectManager;
            _lazyInventoryUiGameObject = new Lazy<GameObject>(() =>
            {
                return unityGameObjectManager
                    .FindAll(x => x.name == "Inventory")
                    .First();
            });
            _dropItemHandler = dropItemHandler;
            _mouseInput = mouseInput;
        }

        private GameObject InventoryUiGameObject => _lazyInventoryUiGameObject.Value;

        public IReadOnlyDragEquipmentItemBehaviour Attach(IEquipSlotPrefab equipSlot)
        {
            var dragInventoryListItemBehaviour = equipSlot.AddComponent<DragEquipmentItemBehaviour>();
            dragInventoryListItemBehaviour.DragItemFactory = _dragItemFactory;
            dragInventoryListItemBehaviour.ObjectDestroyer = _objectDestroyer;
            dragInventoryListItemBehaviour.DropItemHandler = _dropItemHandler;
            dragInventoryListItemBehaviour.GameObjectManager = _gameObjectManager; ;
            dragInventoryListItemBehaviour.MouseInput = _mouseInput;
            dragInventoryListItemBehaviour.InventoryGameObject = InventoryUiGameObject;
            dragInventoryListItemBehaviour.EquipSlot = equipSlot;

            return dragInventoryListItemBehaviour;
        }
    }
}