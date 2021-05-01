using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common.Api
{
    public interface ISyncMacerusToUnityWorldLocationBehaviourStitcher
    {
        ISyncMacerusToUnityWorldLocationBehaviour Stitch(
            IGameObject gameObject,
            GameObject unityGameObject);
    }
}