using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public interface IDragInventoryListItemBehaviourStitcher
    {
        IReadOnlyDragInventoryListItemBehaviour Attach(IInventoryListItemPrefab inventoryListItemGameObject);
    }
}