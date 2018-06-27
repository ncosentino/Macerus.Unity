using Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory;
using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
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

        public IReadonlyHasGuiInventoryBehaviour Attach(
            GameObject gameObject,
            IItemContainerBehavior itemContainerBehavior)
        {
            var hasGuiInventoryBehaviour = gameObject.AddComponent<HasGuiInventoryBehaviour>();
            hasGuiInventoryBehaviour.GameObjectManager = _gameObjectManager;
            hasGuiInventoryBehaviour.ItemListFactory = _itemListFactory;
            hasGuiInventoryBehaviour.ObjectDestroyer = _objectDestroyer;
            hasGuiInventoryBehaviour.ItemContainerBehavior = itemContainerBehavior;
            return hasGuiInventoryBehaviour;
        }
    }
}