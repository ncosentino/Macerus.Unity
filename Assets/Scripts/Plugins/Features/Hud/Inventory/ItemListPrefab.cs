using System;
using System.Linq;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public sealed class ItemListPrefab : IItemListPrefab
    {
        private readonly Lazy<GameObject> _content;

        public ItemListPrefab(GameObject gameObject)
        {
            GameObject = gameObject;
            _content = new Lazy<GameObject>(() => gameObject
                .GetChildGameObjects()
                .Single(x => x.name == "ItemListContent"));
        }

        public GameObject GameObject { get; }

        public GameObject Content => _content.Value;
    }
}