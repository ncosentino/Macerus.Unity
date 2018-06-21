using Assets.Scripts.Unity.Resources;
using UnityEngine;

namespace Assets.Scripts.Gui.Hud.Inventory
{
    public interface IItemListBehaviour : IReadonlyItemListBehaviour
    {
        new IPrefabCreator PrefabCreator { get; set; }

        new string ItemListEntryPrefabResource { get; set; }

        new GameObject ListControlContent { get; set; }
    }
}