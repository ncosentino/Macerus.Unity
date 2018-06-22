using System.Linq;
using Assets.Scripts.Gui.Hud.Inventory;
using Assets.Scripts.Scenes.Explore.GameObjects;
using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Framework;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    using IGameObjectManager = Unity.GameObjects.IGameObjectManager;

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

            // FIXME: this is obviously a hack... how do we control which container to find?
            var targetContainerId = new StringIdentifier("Inventory");

            var itemContainerBehavior = gameObject
                .GetRequiredComponent<IHasGameObject>()
                .GameObject
                .Get<IItemContainerBehavior>()
                .Single(x => targetContainerId.Equals(x.ContainerId));

            hasGuiInventoryBehaviour.ItemContainerBehavior = itemContainerBehavior;

            return hasGuiInventoryBehaviour;
        }
    }
}