using System;
using System.Linq;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
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

        public IReadOnlyDragInventoryListItemBehaviour Attach(GameObject inventoryListItemGameObject)
        {
            var dragInventoryListItemBehaviour = inventoryListItemGameObject.AddComponent<DragInventoryListItemBehaviour>();
            dragInventoryListItemBehaviour.DragItemFactory = _dragItemFactory;
            dragInventoryListItemBehaviour.ObjectDestroyer = _objectDestroyer;
            dragInventoryListItemBehaviour.InventoryGameObject = InventoryUiGameObject;

            return dragInventoryListItemBehaviour;
        }
    }
}