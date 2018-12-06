using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IInventoryListItemMutator
    {
        void Mutate(
            GameObject inventoryListItemGameObject,
            IGameObject item);
    }
}