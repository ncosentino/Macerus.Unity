using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors
{
    public interface IDrawWalkPathBehaviourStitcher
    {
        IDrawWalkPathBehaviour Stitch(
            IGameObject gameObject,
            GameObject unityGameObject);
    }
}