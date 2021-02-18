using System;

using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IReadOnlyDragInventoryListItemBehaviour
    {
        event EventHandler<DroppedEventArgs> Dropped;

        GameObject InventoryGameObject { get; }

        IDragItemFactory DragItemFactory { get; }

        IObjectDestroyer ObjectDestroyer { get; }

        IInventoryListItemPrefab InventoryListItem { get; }

        IMouseInput MouseInput { get; }
    }
}