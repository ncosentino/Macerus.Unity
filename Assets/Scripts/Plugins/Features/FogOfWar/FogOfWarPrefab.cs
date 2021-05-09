using System;
using System.Linq;

using Assets.Scripts.Unity.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.FogOfWar
{
    public sealed class FogOfWarPrefab : IFogOfWarPrefab
    {
        private readonly Lazy<RectTransform> _lazyRenderRextureTransform;
        private readonly Lazy<Camera> _lazyFogCameraMain;
        private readonly Lazy<Camera> _lazyFogCameraSecondary;

        public FogOfWarPrefab(GameObject gameObject)
        {
            GameObject = gameObject;
            _lazyRenderRextureTransform = new Lazy<RectTransform>(() =>
                gameObject
                    .GetChildGameObjects(false)
                    .First(x => x.name == "FogOfWarRenderTexture")
                    .GetRequiredComponent<RectTransform>());
            _lazyFogCameraMain = new Lazy<Camera>(() =>
                gameObject.GetRequiredComponentInChild<Camera>("FogOfWarMainCamera"));
            _lazyFogCameraSecondary = new Lazy<Camera>(() =>
                gameObject.GetRequiredComponentInChild<Camera>("FogOfWarSecondaryCamera"));
        }

        public GameObject GameObject { get; }

        public RectTransform FogMainTextureTransform => _lazyRenderRextureTransform.Value;

        public Camera FogCameraMain => _lazyFogCameraMain.Value;

        public Camera FogCameraSecondary => _lazyFogCameraSecondary.Value;
    }
}
