using System;
using Assets.Scripts.Unity.GameObjects;

using NexusLabs.Contracts;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public sealed class FadeAndKillBehaviour : MonoBehaviour
    {
        public double StartOpacity { get; set; }

        public double TargetOpacity { get; set; }

        public TimeSpan FadeOutDuration { get; set; }

        public IObjectDestroyer ObjectDestroyer { get; set; }

        public IFader Fader { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                ObjectDestroyer,
                $"{nameof(ObjectDestroyer)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                Fader,
                $"{nameof(Fader)} was not set on '{gameObject}.{this}'.");
            Contract.Requires(
                FadeOutDuration >= TimeSpan.Zero,
                $"{nameof(FadeOutDuration)} was not greater than or equal to zero on '{gameObject}.{this}'.");

            var image = gameObject.GetRequiredComponent<Image>();

            StartCoroutine(Fader.FadeTo(
                image,
                (float)StartOpacity,
                (float)TargetOpacity,
                (float)FadeOutDuration.TotalSeconds,
                () =>
                {
                    // remove the whole object
                    ObjectDestroyer.Destroy(gameObject);
                }));
        }
    }
}