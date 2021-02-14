using System;
using System.Linq;

using Assets.Scripts.Plugins.Features.Hud.Api;
using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class DragInventoryListItemBehaviourStitcher : IDragInventoryListItemBehaviourStitcher
    {
        private readonly IDragItemFactory _dragItemFactory;
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IDropItemHandler _dropItemHandler;
        private readonly IMouseInput _mouseInput;
        private readonly Lazy<GameObject> _lazyInventoryUiGameObject;

        public DragInventoryListItemBehaviourStitcher(
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
            _dropItemHandler = dropItemHandler;
            _mouseInput = mouseInput;
            _lazyInventoryUiGameObject = new Lazy<GameObject>(() =>
            {
                return unityGameObjectManager
                    .FindAll(x => x.name == "Inventory")
                    .First();
            });
        }

        private GameObject InventoryUiGameObject => _lazyInventoryUiGameObject.Value;

        public IReadOnlyDragInventoryListItemBehaviour Attach(IInventoryListItemPrefab inventoryListItemGameObject)
        {
            var dragInventoryListItemBehaviour = inventoryListItemGameObject
                .GameObject
                .AddComponent<DragInventoryListItemBehaviour>();
            dragInventoryListItemBehaviour.DragItemFactory = _dragItemFactory;
            dragInventoryListItemBehaviour.ObjectDestroyer = _objectDestroyer;
            dragInventoryListItemBehaviour.DropItemHandler = _dropItemHandler;
            dragInventoryListItemBehaviour.MouseInput = _mouseInput;
            dragInventoryListItemBehaviour.GameObjectManager = _gameObjectManager;
            dragInventoryListItemBehaviour.InventoryGameObject = InventoryUiGameObject;
            dragInventoryListItemBehaviour.InventoryListItem = inventoryListItemGameObject;

            return dragInventoryListItemBehaviour;
        }
    }
}