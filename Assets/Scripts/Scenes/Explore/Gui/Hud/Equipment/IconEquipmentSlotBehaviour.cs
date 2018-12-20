using System;
using System.Linq;
using Assets.Scripts.Unity.Resources.Sprites;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Events;
using ProjectXyz.Framework.Contracts;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Equipment
{
    public class IconEquipmentSlotBehaviour :
        MonoBehaviour,
        IIconEquipmentSlotBehaviour
    {
        public ICanEquipBehavior CanEquipBehavior { get; set; }

        public IIdentifier TargetEquipSlotId { get; set; }

        public Image EquipmentIcon { get; set; }

        public ISpriteLoader SpriteLoader { get; set; }

        public string EmptyIconResource { get; set; }

        public void Start()
        {
            Contract.RequiresNotNull(
                TargetEquipSlotId,
                $"{nameof(TargetEquipSlotId)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                EquipmentIcon,
                $"{nameof(EquipmentIcon)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                SpriteLoader,
                $"{nameof(SpriteLoader)} was not set on '{gameObject}.{this}'.");

            if (CanEquipBehavior != null)
            {
                CanEquipBehavior.Equipped += CanEquipBehavior_Equipped;
                CanEquipBehavior.Unequipped += CanEquipBehavior_Unequipped;
            }

            OnUnequipped();
        }

        public void OnDestroy()
        {
            if (CanEquipBehavior != null)
            {
                CanEquipBehavior.Equipped -= CanEquipBehavior_Equipped;
                CanEquipBehavior.Unequipped -= CanEquipBehavior_Unequipped;
            }
        }

        private void OnUnequipped()
        {
            var sprite = SpriteLoader.GetSpriteFromTexture2D(EmptyIconResource);
            EquipmentIcon.sprite = sprite;
        }

        private void CanEquipBehavior_Unequipped(
            object sender,
            EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>> e)
        {
            if (!Equals(e.Data.Item3, TargetEquipSlotId))
            {
                return;
            }

            OnUnequipped();
        }

        private void CanEquipBehavior_Equipped(
            object sender,
            EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>> e)
        {
            if (!Equals(e.Data.Item3, TargetEquipSlotId))
            {
                return;
            }

            var hasInventoryIcon = e
                .Data
                .Item2
                .Owner
                .Behaviors
                .Get<IHasInventoryIcon>()
                .FirstOrDefault();
            var iconResource = hasInventoryIcon != null
                ? hasInventoryIcon.IconResource
                : EmptyIconResource;
            var sprite = SpriteLoader.GetSpriteFromTexture2D(iconResource);
            EquipmentIcon.sprite = sprite;
        }
    }
}
