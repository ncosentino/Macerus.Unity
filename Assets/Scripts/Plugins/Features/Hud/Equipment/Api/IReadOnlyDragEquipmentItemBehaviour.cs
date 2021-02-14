using Assets.Scripts.Plugins.Features.Hud.Api;
using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment.Api
{
    public interface IReadOnlyDragEquipmentItemBehaviour
    {
        GameObject InventoryGameObject { get; }

        IDragItemFactory DragItemFactory { get; }

        IObjectDestroyer ObjectDestroyer { get; }

        IEquipSlotPrefab EquipSlot { get; }

        IDropItemHandler DropItemHandler { get; }

        IGameObjectManager GameObjectManager { get; }

        IMouseInput MouseInput { get; }
    }
}