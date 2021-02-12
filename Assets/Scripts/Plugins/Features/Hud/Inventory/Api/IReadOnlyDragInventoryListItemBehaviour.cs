using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IReadOnlyDragInventoryListItemBehaviour
    {
        GameObject InventoryGameObject { get; }

        IDragItemFactory DragItemFactory { get; }

        IObjectDestroyer ObjectDestroyer { get; }

        IInventoryListItemPrefab InventoryListItem { get; }
    }
}