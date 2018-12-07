using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using Assets.Scripts.Unity.Resources.Sprites;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
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
        private readonly IDropEquipmentSlotBehaviourStitcher _dropEquipmentSlotBehaviourStitcher;

        public EquipmentSlotsFactory(
            IPrefabCreator prefabCreator,
            ISpriteLoader spriteLoader,
            IDropEquipmentSlotBehaviourStitcher dropEquipmentSlotBehaviourStitcher,
            ILogger logger)
        {
            _prefabCreator = prefabCreator;
            _spriteLoader = spriteLoader;
            _dropEquipmentSlotBehaviourStitcher = dropEquipmentSlotBehaviourStitcher;
            _logger = logger;
        }

        public IEnumerable<GameObject> CreateEquipmentSlots(
            IHasEquipmentBehavior hasEquipmentBehavior,
            ICanEquipBehavior canEquipBehavior)
        {
            var slotsViewModelProvider = new EquipmentSlotViewModelProvider();
            var viewModels = slotsViewModelProvider
                .GetViewModels()
                .ToDictionary(x => x.EquipSlotId, x => x);

            foreach (var equipSlotId in hasEquipmentBehavior.SupportedEquipSlotIds)
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
                equipmentSlotGameObject.name = $"Equipment Slot: {equipmentSlotViewModel.EquipSlotId}";
                
                var sprite = _spriteLoader.GetSpriteFromTexture2D(equipmentSlotViewModel.EmptyIconResource);
                equipmentSlotGameObject
                    .GetRequiredComponentInChild<Image>("ActiveIcon")
                    .sprite = sprite;

                if (canEquipBehavior != null)
                {
                    _dropEquipmentSlotBehaviourStitcher.Attach(
                        equipmentSlotGameObject,
                        equipmentSlotViewModel.EquipSlotId,
                        canEquipBehavior);
                }

                // set margin
                var transform = equipmentSlotGameObject.GetComponent<RectTransform>();
                transform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, equipmentSlotViewModel.X, transform.rect.width);
                transform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, equipmentSlotViewModel.Y, transform.rect.height);

                yield return equipmentSlotGameObject;
            }
        }
    }
}