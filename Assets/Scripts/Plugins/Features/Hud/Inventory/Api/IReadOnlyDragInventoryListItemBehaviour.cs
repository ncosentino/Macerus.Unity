﻿using Assets.Scripts.Plugins.Features.Hud.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IReadOnlyDragInventoryListItemBehaviour
    {
        GameObject InventoryGameObject { get; }

        IDragItemFactory DragItemFactory { get; }

        IObjectDestroyer ObjectDestroyer { get; }

        IInventoryListItemPrefab InventoryListItem { get; }

        IDropItemHandler DropItemHandler { get; }

        IMouseInput MouseInput { get; }
    }
}