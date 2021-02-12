using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IDragInventoryListItemBehaviour : IReadOnlyDragInventoryListItemBehaviour
    {
        new GameObject InventoryGameObject { get; set; }

        new IDragItemFactory DragItemFactory { get; set; }

        new IObjectDestroyer ObjectDestroyer { get; set; }

        new IInventoryListItemPrefab InventoryListItem { get; set; }
    }
}