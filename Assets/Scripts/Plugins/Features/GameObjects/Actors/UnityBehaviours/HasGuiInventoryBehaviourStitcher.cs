using Assets.Scripts.Plugins.Features.Hud.Inventory;
using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

using ILogger = ProjectXyz.Api.Logging.ILogger;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public sealed class HasGuiInventoryBehaviourStitcher : IHasGuiInventoryBehaviourStitcher
    {
        private readonly ILogger _logger;
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IItemListFactory _itemListFactory;
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly IViewWelderFactory _viewWelderFactory;

        public HasGuiInventoryBehaviourStitcher(
            IGameObjectManager gameObjectManager,
            IItemListFactory itemListFactory,
            IObjectDestroyer objectDestroyer,
            IViewWelderFactory viewWelderFactory,
            ILogger logger)
        {
            _gameObjectManager = gameObjectManager;
            _itemListFactory = itemListFactory;
            _objectDestroyer = objectDestroyer;
            _viewWelderFactory = viewWelderFactory;
            _logger = logger;
        }

        public IReadonlyHasGuiInventoryBehaviour Attach(
            GameObject gameObject,
            IItemContainerBehavior itemContainerBehavior)
        {
            var hasGuiInventoryBehaviour = gameObject.AddComponent<HasGuiInventoryBehaviour>();
            hasGuiInventoryBehaviour.GameObjectManager = _gameObjectManager;
            hasGuiInventoryBehaviour.ItemListFactory = _itemListFactory;
            hasGuiInventoryBehaviour.ObjectDestroyer = _objectDestroyer;
            hasGuiInventoryBehaviour.ViewWelderFactory = _viewWelderFactory;
            hasGuiInventoryBehaviour.ItemContainerBehavior = itemContainerBehavior;

            _logger.Debug($"'{this}' has attached '{hasGuiInventoryBehaviour}' to '{gameObject}'.");
            return hasGuiInventoryBehaviour;
        }
    }
}