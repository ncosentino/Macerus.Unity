using System;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public sealed class FadeAndKillBehaviourStitcher : IFadeAndKillBehaviourStitcher
    {
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly IFader _fader;

        public FadeAndKillBehaviourStitcher(
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
            var fadeAndKillBehaviour = gameObject.AddComponent<FadeAndKillBehaviour>();
            fadeAndKillBehaviour.ObjectDestroyer = _objectDestroyer;
            fadeAndKillBehaviour.Fader = _fader;
            fadeAndKillBehaviour.FadeOutDuration = transitionDuration;
            fadeAndKillBehaviour.StartOpacity = startOpacity;
            fadeAndKillBehaviour.TargetOpacity = targetOpacity;
        }
    }
}