using Assets.Scripts.Unity.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class DragItemFactory : IDragItemFactory
    {
        private const float ALPHA_SCALE = 2f / 3f;
        private const string DRAG_ITEM_PREFAB_RESOURCE = "Gui/Prefabs/Inventory/InventoryDragItem";

        private readonly IPrefabCreator _prefabCreator;

        public DragItemFactory(IPrefabCreator prefabCreator)
        {
            _prefabCreator = prefabCreator;
        }

        public IDragItemPrefab Create(Image sourceIcon)
        {
            var dragObject = _prefabCreator.CreatePrefab<IDragItemPrefab>(DRAG_ITEM_PREFAB_RESOURCE);
            dragObject.GameObject.name = $"Drag Item";

            var sprite = sourceIcon.sprite;
            dragObject.Icon.sprite = sprite;
            dragObject.Icon.color = new Color(
                sourceIcon.color.r,
                sourceIcon.color.g,
                sourceIcon.color.b,
                sourceIcon.color.a * ALPHA_SCALE);

            return dragObject;
        }
    }
}