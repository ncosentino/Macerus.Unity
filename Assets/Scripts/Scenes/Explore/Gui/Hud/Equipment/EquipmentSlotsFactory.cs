using System.Collections.Generic;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using Assets.Scripts.Unity.Resources.Sprites;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public sealed class EquipmentSlotsFactory : IEquipmentSlotsFactory
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly ISpriteLoader _spriteLoader;

        public EquipmentSlotsFactory(
            IPrefabCreator prefabCreator,
            ISpriteLoader spriteLoader)
        {
            _prefabCreator = prefabCreator;
            _spriteLoader = spriteLoader;
        }

        public IEnumerable<GameObject> CreateEquipmentSlots()
        {
            var slotsViewModelProvider = new EquipmentSlotViewModelProvider();

            foreach (var equipmentSlotViewModel in slotsViewModelProvider.GetViewModels())
            {
                var equipmentSlotGameObject = _prefabCreator.Create<GameObject>(equipmentSlotViewModel.PrefabResource);

                var sprite = _spriteLoader.GetSpriteFromTexture2D(equipmentSlotViewModel.EmptyIconResource);
                equipmentSlotGameObject
                    .GetRequiredComponentInChild<Image>("ActiveIcon")
                    .sprite = sprite;

                // set margin
                var transform = equipmentSlotGameObject.GetComponent<RectTransform>();
                transform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, equipmentSlotViewModel.X, transform.rect.width);
                transform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, equipmentSlotViewModel.Y, transform.rect.height);

                yield return equipmentSlotGameObject;
            }
        }
    }
}