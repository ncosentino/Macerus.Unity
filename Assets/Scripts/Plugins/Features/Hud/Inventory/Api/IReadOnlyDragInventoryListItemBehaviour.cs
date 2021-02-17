using Assets.Scripts.Plugins.Features.Hud.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;

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

        IGameObjectManager GameObjectManager { get; }

        ISocketPatternHandlerFacade SocketPatternHandler { get; }

        ISocketableInfoFactory SocketableInfoFactory { get; }
    }
}