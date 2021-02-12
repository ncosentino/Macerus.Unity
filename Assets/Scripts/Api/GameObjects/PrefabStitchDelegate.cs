using ProjectXyz.Api.Framework;

using UnityEngine;

namespace Assets.Scripts.Api.GameObjects
{
    public delegate void PrefabStitchDelegate(
        GameObject gameObject,
        IIdentifier prefabResourceId);
}