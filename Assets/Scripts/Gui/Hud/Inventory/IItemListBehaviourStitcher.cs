using Assets.Scripts.Plugins.Features.Actors;
using Assets.Scripts.Wip;
using UnityEngine;

namespace Assets.Scripts.Gui.Hud.Inventory
{
    public interface IItemListBehaviourStitcher
    {
        IReadonlyItemListBehaviour Attach(
            GameObject listControl,
            GameObject listControlContent,
            string itemListEntryPrefabResource,
            IItemContainer itemContainer);
    }
}