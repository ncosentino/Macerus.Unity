using Assets.Scripts.Plugins.Features.Actors;
using Assets.Scripts.Unity.Resources;
using Assets.Scripts.Wip;
using UnityEngine;

namespace Assets.Scripts.Gui.Hud.Inventory
{
    public interface IReadonlyItemListBehaviour
    {
        IPrefabCreator PrefabCreator { get; }

        string ItemListEntryPrefabResource { get; }

        GameObject ListControlContent { get; }

        IItemContainer ItemContainer { get; }
    }
}