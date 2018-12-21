using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
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

        public GameObject Create(
            Image sourceIcon,
            IItemContainerBehavior sourceContainerBehavior)
        {
            var dragObject = _prefabCreator.Create<GameObject>(DRAG_ITEM_PREFAB_RESOURCE);
            dragObject.name = $"Drag Item";

            var sprite = sourceIcon.sprite;
            var targetIcon = dragObject.GetRequiredComponentInChild<Image>("Icon");
            targetIcon.sprite = sprite;
            targetIcon.color = new Color(
                sourceIcon.color.r,
                sourceIcon.color.g,
                sourceIcon.color.b,
                sourceIcon.color.a * ALPHA_SCALE);

            return dragObject;
        }
    }
}