using System;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public sealed class FadeInBehaviourStitcher : IFadeInBehaviourStitcher
    {
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly IFader _fader;

        public FadeInBehaviourStitcher(
            IObjectDestroyer objectDestroyer,
            IFader fader)
        {
            _objectDestroyer = objectDestroyer;
            _fader = fader;
        }

        public void Attach(
            GameObject gameObject,
            TimeSpan transitionDuration,
            double startOpacity,
            double targetOpacity)
        {
            var fadeAndKillBehaviour = gameObject.AddComponent<FadeInBehaviour>();
            fadeAndKillBehaviour.ObjectDestroyer = _objectDestroyer;
            fadeAndKillBehaviour.Fader = _fader;
            fadeAndKillBehaviour.FadeInDuration = transitionDuration;
            fadeAndKillBehaviour.StartOpacity = startOpacity;
            fadeAndKillBehaviour.TargetOpacity = targetOpacity;
        }
    }
}