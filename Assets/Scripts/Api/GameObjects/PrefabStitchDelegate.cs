using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Api.GameObjects
{
    public delegate void PrefabStitchDelegate(
        GameObject unityGameObject,
        IGameObject gameObject,
        IIdentifier prefabResourceId);
}