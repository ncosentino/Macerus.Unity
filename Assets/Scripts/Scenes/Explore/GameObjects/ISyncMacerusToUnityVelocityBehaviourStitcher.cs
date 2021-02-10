using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface ISyncMacerusToUnityVelocityBehaviourStitcher
    {
        ISyncMacerusToUnityVelocityBehaviour Stitch(
            IGameObject gameObject,
            GameObject unityGameObject);
    }
}