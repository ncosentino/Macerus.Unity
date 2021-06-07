using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Api.GameObjects
{
    public interface IPrefabStitcher
    {
        void Stitch(
            GameObject unityGameObject,
            IGameObject gameObject,
            IIdentifier prefabResourceId);
    }
}