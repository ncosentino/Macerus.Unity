using System.Linq;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources.Sprites;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;
using UnityEngine.UI;
using ILogger = ProjectXyz.Api.Logging.ILogger;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
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
            GameObject equipSlotGameObject,
            IIdentifier targetEquipSlotId,
            ICanEquipBehavior canEquipBehavior,
            string emptyIconResource)
        {
            var iconEquipmentSlotBehaviour = equipSlotGameObject.AddComponent<IconEquipmentSlotBehaviour>();

            var icon = equipSlotGameObject.GetRequiredComponentInChild<Image>("ActiveIcon");
            iconEquipmentSlotBehaviour.EquipmentIcon = icon;
            iconEquipmentSlotBehaviour.TargetEquipSlotId = targetEquipSlotId;
            iconEquipmentSlotBehaviour.CanEquipBehavior = canEquipBehavior;
            iconEquipmentSlotBehaviour.EmptyIconResource = emptyIconResource;
            iconEquipmentSlotBehaviour.SpriteLoader = _spriteLoader;

            _logger.Debug($"'{iconEquipmentSlotBehaviour}' attached to '{equipSlotGameObject}'.");
            return iconEquipmentSlotBehaviour;
        }
    }
}