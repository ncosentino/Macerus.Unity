using System;
using System.Linq;

using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class DragInventoryListItemBehaviourStitcher : IDragInventoryListItemBehaviourStitcher
    {
        private readonly IDragItemFactory _dragItemFactory;
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly Lazy<GameObject> _lazyInventoryUiGameObject;

        public DragInventoryListItemBehaviourStitcher(
            IDragItemFactory dragItemFactory,
            IObjectDestroyer objectDestroyer,
            IGameObjectManager gameObjectManager)
        {
            _dragItemFactory = dragItemFactory;
            _objectDestroyer = objectDestroyer;
            _lazyInventoryUiGameObject = new Lazy<GameObject>(() =>
            {
                return gameObjectManager
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
            dragInventoryListItemBehaviour.InventoryGameObject = InventoryUiGameObject;
            dragInventoryListItemBehaviour.InventoryListItem = inventoryListItemGameObject;

            return dragInventoryListItemBehaviour;
        }
    }
}