using System;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Plugins.Features.Hud.Equipment
{
    public sealed class EquipSlotPrefab : IEquipSlotPrefab
    {
        private readonly Lazy<Image> _icon;
        private readonly Lazy<Image> _background;

        public EquipSlotPrefab(GameObject gameObject)
        {
            GameObject = gameObject;
            _icon = new Lazy<Image>(() => gameObject
                .GetRequiredComponentInChild<Image>("ActiveIcon"));
            _background = new Lazy<Image>(() => gameObject
                .GetRequiredComponentInChild<Image>("Background"));
        }

        public GameObject GameObject { get; }

        public Image ActiveIcon => _icon.Value;

        public Image Background => _background.Value;
    }
}