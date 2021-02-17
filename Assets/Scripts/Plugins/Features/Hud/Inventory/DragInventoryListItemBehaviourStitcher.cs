using System;
using System.Linq;

using Assets.Scripts.Plugins.Features.Hud.Api;
using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class DragInventoryListItemBehaviourStitcher : IDragInventoryListItemBehaviourStitcher
    {
        private readonly IDragItemFactory _dragItemFactory;
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly IUnityGameObjectManager _unityGameObjectManager;
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IDropItemHandler _dropItemHandler;
        private readonly IMouseInput _mouseInput;
        private readonly ISocketPatternHandlerFacade _socketPatternHandler;
        private readonly ISocketableInfoFactory _socketableInfoFactory;
        private readonly Lazy<GameObject> _lazyInventoryUiGameObject;

        public DragInventoryListItemBehaviourStitcher(
            IDragItemFactory dragItemFactory,
            IObjectDestroyer objectDestroyer,
            IUnityGameObjectManager unityGameObjectManager,
            IGameObjectManager gameObjectManager,
            IDropItemHandler dropItemHandler,
            IMouseInput mouseInput,
            ISocketPatternHandlerFacade socketPatternHandler,
            ISocketableInfoFactory socketableInfoFactory)
        {
            _dragItemFactory = dragItemFactory;
            _objectDestroyer = objectDestroyer;
            _unityGameObjectManager = unityGameObjectManager;
            _gameObjectManager = gameObjectManager;
            _dropItemHandler = dropItemHandler;
            _mouseInput = mouseInput;
            _socketPatternHandler = socketPatternHandler;
            _socketableInfoFactory = socketableInfoFactory;
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
            dragInventoryListItemBehaviour.SocketableInfoFactory = _socketableInfoFactory;
            dragInventoryListItemBehaviour.SocketPatternHandler = _socketPatternHandler;
            dragInventoryListItemBehaviour.InventoryGameObject = InventoryUiGameObject;
            dragInventoryListItemBehaviour.InventoryListItem = inventoryListItemGameObject;

            return dragInventoryListItemBehaviour;
        }
    }
}