using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public sealed class Fader : IFader
    {
        // Define an enumerator to perform our fading.
        // Pass it the material to fade, the opacity to fade to (0 = transparent, 1 = opaque),
        // and the number of seconds to fade over.
        public IEnumerator FadeTo(
            Image image,
            float startOpacity,
            float targetOpacity,
            float duration,
            Action callback)
        {
            // Cache the current color of the material, and its initiql opacity.
            Color color = image?.color ?? Color.black;

            // Track how many seconds we've been fading.
            float t = 0;

            while (t < duration)
            {
                // Step the fade forward one frame.
                t += Time.deltaTime;
                // Turn the time into an interpolation factor between 0 and 1.
                float blend = Mathf.Clamp01(t / duration);

                // Blend to the corresponding opacity between start & target.
                color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);

                // Apply the resulting color to the material.
                if (image != null)
                {
                    image.color = color;
                }

                // Wait one frame, and repeat.
                yield return null;
            }

            callback?.Invoke();
        }
    }
}