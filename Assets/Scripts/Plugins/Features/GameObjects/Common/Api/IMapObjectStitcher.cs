using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common.Api
{
    public interface IMapObjectStitcher
    {
        void Stitch(
            IGameObject gameObject,
            GameObject unityGameObject,
            IIdentifier prefabResourceId);
    }
}