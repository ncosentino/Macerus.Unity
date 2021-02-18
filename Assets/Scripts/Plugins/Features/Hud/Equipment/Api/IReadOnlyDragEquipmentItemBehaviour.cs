using System;

using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment.Api
{
    public interface IReadOnlyDragEquipmentItemBehaviour
    {
        event EventHandler<DroppedEventArgs> Dropped;

        GameObject InventoryGameObject { get; }

        IDragItemFactory DragItemFactory { get; }

        IObjectDestroyer ObjectDestroyer { get; }

        IEquipSlotPrefab EquipSlot { get; }

        IMouseInput MouseInput { get; }
    }
}