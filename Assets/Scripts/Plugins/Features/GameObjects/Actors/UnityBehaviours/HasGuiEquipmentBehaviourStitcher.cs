using Assets.Scripts.Plugins.Features.Hud.Equipment;
using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

using ILogger = ProjectXyz.Api.Logging.ILogger;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public sealed class HasGuiEquipmentBehaviourStitcher : IHasGuiEquipmentBehaviourStitcher
    {
        private readonly ILogger _logger;
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IEquipmentSlotsFactory _equipmentSlotsFactory;
        private readonly IObjectDestroyer _objectDestroyer;

        public HasGuiEquipmentBehaviourStitcher(
            IGameObjectManager gameObjectManager,
            IEquipmentSlotsFactory equipmentSlotsFactory,
            IObjectDestroyer objectDestroyer,
            ILogger logger)
        {
            _gameObjectManager = gameObjectManager;
            _equipmentSlotsFactory = equipmentSlotsFactory;
            _objectDestroyer = objectDestroyer;
            _logger = logger;
        }

        public IReadonlyHasGuiEquipmentBehaviour Attach(
            GameObject gameObject,
            IHasEquipmentBehavior hasEquipmentBehavior,
            ICanEquipBehavior canEquipBehavior)
        {
            var hasGuiEquipmentBehaviour = gameObject.AddComponent<HasGuiEquipmentBehaviour>();
            hasGuiEquipmentBehaviour.GameObjectManager = _gameObjectManager;
            hasGuiEquipmentBehaviour.EquipmentSlotsFactory = _equipmentSlotsFactory;
            hasGuiEquipmentBehaviour.ObjectDestroyer = _objectDestroyer;
            hasGuiEquipmentBehaviour.HasEquipmentBehavior = hasEquipmentBehavior;
            hasGuiEquipmentBehaviour.CanEquipBehavior = canEquipBehavior;

            //// TODO: actually pass in the behaviours here so we can do somethin' fance

            _logger.Debug($"'{this}' has attached '{hasGuiEquipmentBehaviour}' to '{gameObject}'.");
            return hasGuiEquipmentBehaviour;
        }
    }
}