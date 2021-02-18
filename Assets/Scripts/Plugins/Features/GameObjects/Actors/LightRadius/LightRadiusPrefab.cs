using Assets.Scripts.Unity.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public sealed class LightRadiusPrefab : ILightRadiusPrefab
    {
        public LightRadiusPrefab(GameObject gameObject)
        {
            GameObject = gameObject;
            Light = gameObject.GetRequiredComponent<Light>();
        }

        public Light Light { get; }

        public GameObject GameObject { get; }
    }
}