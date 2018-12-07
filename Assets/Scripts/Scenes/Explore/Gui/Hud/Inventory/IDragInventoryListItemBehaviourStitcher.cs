using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IDragInventoryListItemBehaviourStitcher
    {
        IReadOnlyDragInventoryListItemBehaviour Attach(GameObject inventoryListItemGameObject);
    }
}