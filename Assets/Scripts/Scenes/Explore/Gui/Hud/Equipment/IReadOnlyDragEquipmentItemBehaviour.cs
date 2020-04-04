using Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public interface IReadOnlyDragEquipmentItemBehaviour
    {
        GameObject InventoryGameObject { get; }

        IDragItemFactory DragItemFactory { get; }

        IObjectDestroyer ObjectDestroyer { get; }

        IEquipSlotPrefab EquipSlot { get; }
    }
}