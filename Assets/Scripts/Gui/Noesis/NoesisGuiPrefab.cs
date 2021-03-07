using System;

using Assets.Scripts.Unity.GameObjects;

using Noesis;

using UnityEngine;

namespace Assets.Scripts.Gui.Noesis
{
    public sealed class NoesisGuiPrefab : INoesisGuiPrefab
    {
        private readonly Lazy<ContentControl> _lazyContainerControl;

        public NoesisGuiPrefab(GameObject gameObject)
        {
            GameObject = gameObject;
            _lazyContainerControl = new Lazy<ContentControl>(() =>
            {
                var viewBox = (ContentControl)GameObject
                    .GetRequiredComponent<NoesisView>()
                    .Content;
                return viewBox;
            });
        }

        public GameObject GameObject { get; }

        public ContentControl ContainerControl => _lazyContainerControl.Value;
    }
}
