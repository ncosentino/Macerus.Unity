using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface ISyncUnityToMacerusWorldLocationBehaviourStitcher
    {
        ISyncUnityToMacerusWorldLocationBehaviour Stitch(IGameObject gameObject, GameObject unityGameObject);
    }
}