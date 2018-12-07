using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using Assets.Scripts.Unity.Resources.Sprites;
using ProjectXyz.Api.Framework;
using UnityEngine;
using UnityEngine.UI;
using ILogger = ProjectXyz.Api.Logging.ILogger;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public sealed class EquipmentSlotsFactory : IEquipmentSlotsFactory
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly ISpriteLoader _spriteLoader;
        private readonly ILogger _logger;

        public EquipmentSlotsFactory(
            IPrefabCreator prefabCreator,
            ISpriteLoader spriteLoader,
            ILogger logger)
        {
            _prefabCreator = prefabCreator;
            _spriteLoader = spriteLoader;
            _logger = logger;
        }

        public IEnumerable<GameObject> CreateEquipmentSlots(IEnumerable<IIdentifier> equipSlotIds)
        {
            var slotsViewModelProvider = new EquipmentSlotViewModelProvider();
            var viewModels = slotsViewModelProvider
                .GetViewModels()
                .ToDictionary(x => x.EquipSlotId, x => x);

            foreach (var equipSlotId in equipSlotIds)
            {
                IEquipmentSlotViewModel equipmentSlotViewModel;
                if (!viewModels.TryGetValue(
                    equipSlotId,
                    out equipmentSlotViewModel))
                {
                    _logger.Error($"There is no equip slot view model for equip slot id '{equipSlotId}'.");
                    continue;
                }

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