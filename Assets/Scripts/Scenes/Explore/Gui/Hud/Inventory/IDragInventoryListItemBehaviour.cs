using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IDragInventoryListItemBehaviour : IReadOnlyDragInventoryListItemBehaviour
    {
        new GameObject InventoryGameObject { get; set; }

        new IDragItemFactory DragItemFactory { get; set; }

        new IObjectDestroyer ObjectDestroyer { get; set; }

        new IInventoryListItemPrefab InventoryListItem { get; set; }
    }
}