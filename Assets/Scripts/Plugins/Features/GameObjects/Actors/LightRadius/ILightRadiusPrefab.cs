using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public interface ILightRadiusPrefab : IPrefab
    {
        Light Light { get; }
    }
}