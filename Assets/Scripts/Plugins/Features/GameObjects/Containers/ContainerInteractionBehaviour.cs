using System.Linq;

using Assets.Scripts.Unity.GameObjects;

using Macerus.Game.GameObjects;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerInteractionBehaviour :
        MonoBehaviour,
        IContainerInteractionBehaviour
    {
        public IGameObjectManager GameObjectManager { get; set; }

        public IObjectDestroyer ObjectDestroyer { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                GameObjectManager,
                $"{nameof(GameObjectManager)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                ObjectDestroyer,
                $"{nameof(ObjectDestroyer)} was not set on '{gameObject}.{this}'.");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.IsPlayerControlled())
            {
                return;
            }

            // FIXME: we want actually user-decided pickup logic, not auto collision to do it
            if (!_leaveOnce)
            {
                Debug.Log("FIXME: improve this logic to not require exit trigger area before re-entering...");
                return;
            }

            TransferLootAndDestroy();
        }

        // FIXME: this is a crappy way to try and force re-entry into a container to auto-pickup...
        // need to figure out how to get some smarter interactions
        private bool _leaveOnce;
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.gameObject.IsPlayerControlled())
            {
                return;
            }

            _leaveOnce = true;
        }

        private void TransferLootAndDestroy()
        {
            var playerInventory = GameObjectManager
                .GetPlayer()
                .Get<IItemContainerBehavior>()
                .Single(x => x.ContainerId.Equals(new StringIdentifier("Inventory")));
            var sourceItemContainer = gameObject.GetOnly<IItemContainerBehavior>();

            // need a copy so we can iterate + remove
            var itemsToTake = sourceItemContainer
                .Items
                .ToArray();
            foreach (var item in itemsToTake)
            {
                sourceItemContainer.TryRemoveItem(item);
                playerInventory.TryAddItem(item);
            }

            // FIXME: we don't necessarily want to auto-destroy all containers
            ObjectDestroyer.Destroy(gameObject);
        }
    }
}
