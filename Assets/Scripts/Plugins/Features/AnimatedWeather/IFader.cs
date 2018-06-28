using System;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public interface IFader
    {
        IEnumerator FadeTo(
            Image image,
            float startOpacity,
            float targetOpacity,
            float duration,
            Action callback);
    }
}