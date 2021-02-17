using Assets.Scripts.Plugins.Features.Hud.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IDragInventoryListItemBehaviour : IReadOnlyDragInventoryListItemBehaviour
    {
        new GameObject InventoryGameObject { get; set; }

        new IDragItemFactory DragItemFactory { get; set; }

        new IObjectDestroyer ObjectDestroyer { get; set; }

        new IInventoryListItemPrefab InventoryListItem { get; set; }

        new IDropItemHandler DropItemHandler { get; set; }

        new IMouseInput MouseInput { get; set; }

        new IGameObjectManager GameObjectManager { get; set; }

        new ISocketPatternHandlerFacade SocketPatternHandler { get; set; }

        new ISocketableInfoFactory SocketableInfoFactory { get; set; }
    }
}