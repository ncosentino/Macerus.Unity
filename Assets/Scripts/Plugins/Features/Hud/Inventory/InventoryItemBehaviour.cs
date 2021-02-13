using System;

using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class InventoryItemBehaviour :
        MonoBehaviour,
        IInventoryItemBehaviour
    {
        public IGameObject InventoryItem { get; set; }

        public Func<IItemContainerBehavior> SourceItemContainerCallback { get; set; }

        public void Start()
        {
            Contract.RequiresNotNull(
                InventoryItem,
                $"{nameof(InventoryItem)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                SourceItemContainerCallback,
                $"{nameof(SourceItemContainerCallback)} was not set on '{gameObject}.{this}'.");
        }

        public bool TryRemoveFromSourceContainer()
        {
            if (!SourceItemContainerCallback().TryRemoveItem(InventoryItem))
            {
                return false;
            }

            return true;
        }
    }
}