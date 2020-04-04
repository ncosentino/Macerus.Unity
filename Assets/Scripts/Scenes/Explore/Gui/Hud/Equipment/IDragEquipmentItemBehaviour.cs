using Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public interface IDragEquipmentItemBehaviour : IReadOnlyDragEquipmentItemBehaviour
    {
        new GameObject InventoryGameObject { get; set; }

        new IDragItemFactory DragItemFactory { get; set; }

        new IObjectDestroyer ObjectDestroyer { get; set; }

        new IEquipSlotPrefab EquipSlot { get; set; }
    }
}