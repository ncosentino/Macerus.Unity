using System;

using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class InventoryListItemPrefab : IInventoryListItemPrefab
    {
        private readonly Lazy<Image> _icon;
        private readonly Lazy<Text> _name;

        public InventoryListItemPrefab(GameObject gameObject)
        {
            GameObject = gameObject;
            _icon = new Lazy<Image>(() => gameObject
                .GetRequiredComponentInChild<Image>("Icon"));
            _name = new Lazy<Text>(() => gameObject
                .GetRequiredComponentInChild<Text>("Name"));
        }

        public GameObject GameObject { get; }

        public Image Icon => _icon.Value;

        public Text Name => _name.Value;
    }
}