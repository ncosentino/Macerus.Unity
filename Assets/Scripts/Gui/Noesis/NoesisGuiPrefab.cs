using System;

using Assets.Scripts.Unity.GameObjects;

using Noesis;

using UnityEngine;

namespace Assets.Scripts.Gui.Noesis
{
    public sealed class NoesisGuiPrefab : INoesisGuiPrefab
    {
        private readonly Lazy<Viewbox> _lazyViewbox;

        public NoesisGuiPrefab(GameObject gameObject)
        {
            GameObject = gameObject;
            _lazyViewbox = new Lazy<Viewbox>(() =>
            {
                var viewBox = (Viewbox)GameObject
                    .GetRequiredComponent<NoesisView>()
                    .Content;
                return viewBox;
            });
        }

        public GameObject GameObject { get; }

        public Viewbox ViewBox => _lazyViewbox.Value;
    }
}
