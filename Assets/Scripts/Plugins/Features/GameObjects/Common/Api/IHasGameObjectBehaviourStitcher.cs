using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common.Api
{
    public interface IHasGameObjectBehaviourStitcher
    {
        IHasGameObject Attach(
            IGameObject gameObject,
            GameObject unityGameObject);
    }
}