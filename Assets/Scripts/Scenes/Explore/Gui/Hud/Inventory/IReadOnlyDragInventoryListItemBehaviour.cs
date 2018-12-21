using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IReadOnlyDragInventoryListItemBehaviour
    {
        GameObject InventoryGameObject { get; }

        IDragItemFactory DragItemFactory { get; }

        IObjectDestroyer ObjectDestroyer { get; }
    }
}