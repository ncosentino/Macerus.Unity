using System;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public interface IFadeAndKillBehaviourStitcher
    {
        void Attach(GameObject gameObject, TimeSpan transitionDuration, double startOpacity, double targetOpacity);
    }
}