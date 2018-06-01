using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IUnityGameObjectRepository
    {
        GameObject Create(IGameObject gameObject);
    }
}