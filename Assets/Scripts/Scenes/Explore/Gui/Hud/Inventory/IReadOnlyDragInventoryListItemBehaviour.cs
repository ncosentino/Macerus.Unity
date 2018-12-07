using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IReadOnlyDragInventoryListItemBehaviour
    {
        GameObject InventoryGameObject { get; }

        IPrefabCreator PrefabCreator { get; }

        IObjectDestroyer ObjectDestroyer { get; }

        string DragItemPrefabResource { get; }
    }
}