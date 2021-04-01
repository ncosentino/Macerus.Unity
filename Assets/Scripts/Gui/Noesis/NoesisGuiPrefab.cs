using System;

using Assets.Scripts.Unity.GameObjects;

using NexusLabs.Contracts;

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
                var noesisView = GameObject.GetRequiredComponent<NoesisView>();
                var contentControl = (ContentControl)noesisView.Content;
                Contract.RequiresNotNull(
                    contentControl,
                    $"The '{nameof(NoesisView.Content)}' property on " +
                    $"'{noesisView}' was not set.");
                return contentControl;
            });
        }

        public GameObject GameObject { get; }

        public ContentControl ContainerControl => _lazyContainerControl.Value;
    }
}
