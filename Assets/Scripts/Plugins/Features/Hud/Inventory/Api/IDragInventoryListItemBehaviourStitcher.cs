using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IDragInventoryListItemBehaviourStitcher
    {
        IReadOnlyDragInventoryListItemBehaviour Attach(IInventoryListItemPrefab inventoryListItemGameObject);
    }
}