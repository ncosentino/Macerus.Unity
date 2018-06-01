using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IMapObjectStitcher
    {
        void Stitch(
            IGameObject gameObject,
            GameObject unityGameObject,
            string prefabResourceId);
    }
}