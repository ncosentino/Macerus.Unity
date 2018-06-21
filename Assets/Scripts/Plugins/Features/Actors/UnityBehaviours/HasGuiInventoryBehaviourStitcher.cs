using Assets.Scripts.Gui.Hud.Inventory;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Wip;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public sealed class HasGuiInventoryBehaviourStitcher : IHasGuiInventoryBehaviourStitcher
    {
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IItemListFactory _itemListFactory;
        private readonly IObjectDestroyer _objectDestroyer;

        public HasGuiInventoryBehaviourStitcher(
            IGameObjectManager gameObjectManager,
            IItemListFactory itemListFactory,
            IObjectDestroyer objectDestroyer)
        {
            _gameObjectManager = gameObjectManager;
            _itemListFactory = itemListFactory;
            _objectDestroyer = objectDestroyer;
        }

        public IReadonlyHasGuiInventoryBehaviour Attach(GameObject gameObject)
        {
            var hasGuiInventoryBehaviour = gameObject.AddComponent<HasGuiInventoryBehaviour>();
            hasGuiInventoryBehaviour.GameObjectManager = _gameObjectManager;
            hasGuiInventoryBehaviour.ItemListFactory = _itemListFactory;
            hasGuiInventoryBehaviour.ObjectDestroyer = _objectDestroyer;

            hasGuiInventoryBehaviour.ItemContainer = new ItemContainer(); // TODO: pull this info from some behavio(u)rs

            return hasGuiInventoryBehaviour;
        }
    }
}