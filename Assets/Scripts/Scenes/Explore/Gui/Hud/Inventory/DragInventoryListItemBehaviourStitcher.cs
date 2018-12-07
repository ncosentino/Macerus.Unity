using System;
using System.Linq;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public sealed class DragInventoryListItemBehaviourStitcher : IDragInventoryListItemBehaviourStitcher
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly Lazy<GameObject> _lazyInventoryUiGameObject;

        public DragInventoryListItemBehaviourStitcher(
            IPrefabCreator prefabCreator,
            IObjectDestroyer objectDestroyer,
            IGameObjectManager gameObjectManager)
        {
            _prefabCreator = prefabCreator;
            _objectDestroyer = objectDestroyer;
            _lazyInventoryUiGameObject = new Lazy<GameObject>(() =>
            {
                return gameObjectManager
                    .FindAll(x => x.name == "Inventory")
                    .First();
            });
        }

        public IReadOnlyDragInventoryListItemBehaviour Attach(GameObject inventoryListItemGameObject)
        {
            var dragInventoryListItemBehaviour = inventoryListItemGameObject.AddComponent<DragInventoryListItemBehaviour>();
            dragInventoryListItemBehaviour.PrefabCreator = _prefabCreator;
            dragInventoryListItemBehaviour.ObjectDestroyer = _objectDestroyer;
            dragInventoryListItemBehaviour.InventoryGameObject = _lazyInventoryUiGameObject.Value;
            dragInventoryListItemBehaviour.DragItemPrefabResource = "Gui/Prefabs/Inventory/InventoryDragItem";

            return dragInventoryListItemBehaviour;
        }
    }
}