using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IReadonlyItemListBehaviour
    {
        IItemToListItemEntryConverter ItemToListItemEntryConverter { get; }

        string ItemListEntryPrefabResource { get; }

        GameObject ListControlContent { get; }

        IItemContainerBehavior ItemContainerBehavior { get; }
    }
}