using System;
using System.Linq;

using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class DragInventoryListItemBehaviourStitcher : IDragInventoryListItemBehaviourStitcher
    {
        private readonly IDragItemFactory _dragItemFactory;
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly IMouseInput _mouseInput;
        private readonly IInventoryItemDropUiFlow _inventoryItemDropUiFlow;
        private readonly Lazy<GameObject> _lazyInventoryUiGameObject;

        public DragInventoryListItemBehaviourStitcher(
            IDragItemFactory dragItemFactory,
            IObjectDestroyer objectDestroyer,
            IUnityGameObjectManager unityGameObjectManager,
            IMouseInput mouseInput,
            IInventoryItemDropUiFlow inventoryItemDropUiFlow)
        {
            _dragItemFactory = dragItemFactory;
            _objectDestroyer = objectDestroyer;
            _mouseInput = mouseInput;
            _inventoryItemDropUiFlow = inventoryItemDropUiFlow;
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
            dragInventoryListItemBehaviour.MouseInput = _mouseInput;
            dragInventoryListItemBehaviour.InventoryGameObject = InventoryUiGameObject;
            dragInventoryListItemBehaviour.InventoryListItem = inventoryListItemGameObject;
            dragInventoryListItemBehaviour.Dropped += (_, e) => _inventoryItemDropUiFlow.Execute(e);

            return dragInventoryListItemBehaviour;
        }
    }
}