using Assets.Scripts.Unity.Resources;
using UnityEngine;

namespace Assets.Scripts.Gui.Hud.Inventory
{
    public interface IReadonlyItemListBehaviour
    {
        IPrefabCreator PrefabCreator { get; }

        string ItemListEntryPrefabResource { get; }

        GameObject ListControlContent { get; }
    }
}