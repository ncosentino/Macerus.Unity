using Assets.Scripts.Plugins.Features.Hud.Api;
using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment.Api
{
    public interface IDragEquipmentItemBehaviour : IReadOnlyDragEquipmentItemBehaviour
    {
        new GameObject InventoryGameObject { get; set; }

        new IDragItemFactory DragItemFactory { get; set; }

        new IObjectDestroyer ObjectDestroyer { get; set; }

        new IEquipSlotPrefab EquipSlot { get; set; }
        
        new IDropItemHandler DropItemHandler { get; set; }

        new IGameObjectManager GameObjectManager { get; set; }

        new IMouseInput MouseInput { get; set; }
    }
}