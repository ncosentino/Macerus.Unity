using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;
using Assets.Scripts.Unity.Resources.Sprites;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using ILogger = ProjectXyz.Api.Logging.ILogger;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment
{
    public class IconEquipmentSlotBehaviourStitcher : IIconEquipmentSlotBehaviourStitcher
    {
        private readonly ISpriteLoader _spriteLoader;
        private readonly ILogger _logger;

        public IconEquipmentSlotBehaviourStitcher(
            ISpriteLoader spriteLoader,
            ILogger logger)
        {
            _spriteLoader = spriteLoader;
            _logger = logger;
        }

        public IReadOnlyIconEquipmentSlotBehaviour Attach(
            IEquipSlotPrefab equipSlotGameObject,
            IIdentifier targetEquipSlotId,
            ICanEquipBehavior canEquipBehavior,
            string emptyIconResource)
        {
            var iconEquipmentSlotBehaviour = equipSlotGameObject
                .GameObject
                .AddComponent<IconEquipmentSlotBehaviour>();

            iconEquipmentSlotBehaviour.EquipmentIcon = equipSlotGameObject.ActiveIcon;
            iconEquipmentSlotBehaviour.TargetEquipSlotId = targetEquipSlotId;
            iconEquipmentSlotBehaviour.CanEquipBehavior = canEquipBehavior;
            iconEquipmentSlotBehaviour.EmptyIconResource = emptyIconResource;
            iconEquipmentSlotBehaviour.SpriteLoader = _spriteLoader;

            _logger.Debug($"'{iconEquipmentSlotBehaviour}' attached to '{equipSlotGameObject}'.");
            return iconEquipmentSlotBehaviour;
        }
    }
}