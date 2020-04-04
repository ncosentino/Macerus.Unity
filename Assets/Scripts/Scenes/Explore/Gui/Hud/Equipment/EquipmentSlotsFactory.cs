using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Unity.Resources;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;
using ILogger = ProjectXyz.Api.Logging.ILogger;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public sealed class EquipmentSlotsFactory : IEquipmentSlotsFactory
    {
        private readonly IPrefabCreator _prefabCreator;
        private readonly ILogger _logger;
        private readonly IDropEquipmentSlotBehaviourStitcher _dropEquipmentSlotBehaviourStitcher;
        private readonly IDragEquipmentItemBehaviourStitcher _dragEquipmentItemBehaviourStitcher;
        private readonly IIconEquipmentSlotBehaviourStitcher _iconEquipmentSlotBehaviourStitcher;

        public EquipmentSlotsFactory(
            IPrefabCreator prefabCreator,
            IIconEquipmentSlotBehaviourStitcher iconEquipmentSlotBehaviourStitcher,
            IDragEquipmentItemBehaviourStitcher dragEquipmentItemBehaviourStitcher,
            IDropEquipmentSlotBehaviourStitcher dropEquipmentSlotBehaviourStitcher,
            ILogger logger)
        {
            _prefabCreator = prefabCreator;
            _iconEquipmentSlotBehaviourStitcher = iconEquipmentSlotBehaviourStitcher;
            _dragEquipmentItemBehaviourStitcher = dragEquipmentItemBehaviourStitcher;
            _dropEquipmentSlotBehaviourStitcher = dropEquipmentSlotBehaviourStitcher;
            _logger = logger;
        }

        public IEnumerable<IEquipSlotPrefab> CreateEquipmentSlots(
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

                var equipmentSlotPrefab = _prefabCreator.CreatePrefab<IEquipSlotPrefab>(equipmentSlotViewModel.PrefabResource);
                equipmentSlotPrefab.GameObject.name = $"Equipment Slot: {equipmentSlotViewModel.EquipSlotId}";

                if (canEquipBehavior != null)
                {
                    _dropEquipmentSlotBehaviourStitcher.Attach(
                        equipmentSlotPrefab,
                        equipmentSlotViewModel.EquipSlotId,
                        canEquipBehavior);
                    _dragEquipmentItemBehaviourStitcher.Attach(equipmentSlotPrefab);
                    _iconEquipmentSlotBehaviourStitcher.Attach(
                        equipmentSlotPrefab,
                        equipmentSlotViewModel.EquipSlotId,
                        canEquipBehavior,
                        equipmentSlotViewModel.EmptyIconResource);
                }

                // set margin
                var transform = equipmentSlotPrefab
                    .GameObject
                    .GetComponent<RectTransform>();
                transform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, equipmentSlotViewModel.X, transform.rect.width);
                transform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, equipmentSlotViewModel.Y, transform.rect.height);

                yield return equipmentSlotPrefab;
            }
        }
    }
}