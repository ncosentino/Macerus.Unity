using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public interface ILightRadiusStitcher
    {
        ILightRadiusPrefab Stitch(IGameObject gameObject, GameObject unityGameObject);
    }
}