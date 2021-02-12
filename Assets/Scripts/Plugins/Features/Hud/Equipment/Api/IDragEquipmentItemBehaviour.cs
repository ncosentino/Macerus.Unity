using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment.Api
{
    public interface IDragEquipmentItemBehaviour : IReadOnlyDragEquipmentItemBehaviour
    {
        new GameObject InventoryGameObject { get; set; }

        new IDragItemFactory DragItemFactory { get; set; }

        new IObjectDestroyer ObjectDestroyer { get; set; }

        new IEquipSlotPrefab EquipSlot { get; set; }
    }
}