using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IItemListBehaviour : IReadonlyItemListBehaviour
    {
        new IItemToListItemEntryConverter ItemToListItemEntryConverter { get; set; }

        new string ItemListEntryPrefabResource { get; set; }

        new GameObject ListControlContent { get; set; }

        new IItemContainerBehavior ItemContainerBehavior { get; set; }
    }
}