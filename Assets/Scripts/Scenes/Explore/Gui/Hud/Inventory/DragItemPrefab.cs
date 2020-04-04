using System;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
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