using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment.Api
{
    public interface IReadOnlyDragEquipmentItemBehaviour
    {
        GameObject InventoryGameObject { get; }

        IDragItemFactory DragItemFactory { get; }

        IObjectDestroyer ObjectDestroyer { get; }

        IEquipSlotPrefab EquipSlot { get; }
    }
}