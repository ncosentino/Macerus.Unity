using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common.Api
{
    public interface IUnityGameObjectRepository
    {
        GameObject Create(IGameObject gameObject);
    }
}