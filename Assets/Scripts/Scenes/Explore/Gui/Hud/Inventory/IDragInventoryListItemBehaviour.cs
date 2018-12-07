using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IDragInventoryListItemBehaviour : IReadOnlyDragInventoryListItemBehaviour
    {
        new GameObject InventoryGameObject { get; set; }

        new IPrefabCreator PrefabCreator { get; set; }

        new IObjectDestroyer ObjectDestroyer { get; set; }

        new string DragItemPrefabResource { get; set; }
    }
}