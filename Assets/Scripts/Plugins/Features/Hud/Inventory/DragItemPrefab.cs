using System;

using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class DragItemPrefab : IDragItemPrefab
    {
        private readonly Lazy<Image> _icon;

        public DragItemPrefab(GameObject gameObject)
        {
            GameObject = gameObject;
            _icon = new Lazy<Image>(() => gameObject
                .GetRequiredComponentInChild<Image>("Icon"));
        }

        public GameObject GameObject { get; }

        public Image Icon => _icon.Value;
    }
}