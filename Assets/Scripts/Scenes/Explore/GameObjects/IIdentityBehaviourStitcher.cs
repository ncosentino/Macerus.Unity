using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IIdentityBehaviourStitcher
    {
        void Stitch(
            IGameObject gameObject,
            GameObject unityGameObject);
    }
}