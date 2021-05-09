using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.FogOfWar
{
    public interface IFogOfWarPrefab : IPrefab
    {
        Camera FogCameraMain { get; }

        Camera FogCameraSecondary { get; }

        RectTransform FogMainTextureTransform { get; }
    }
}