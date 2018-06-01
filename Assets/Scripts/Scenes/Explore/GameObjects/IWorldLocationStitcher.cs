using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IWorldLocationStitcher
    {
        IUpdateWorldLocatiobBehaviour Stitch(
            IGameObject gameObject,
            GameObject unityGameObject);
    }
}