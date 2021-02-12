using ProjectXyz.Api.Framework;

using UnityEngine;

namespace Assets.Scripts.Api.GameObjects
{
    public interface IPrefabStitcher
    {
        void Stitch(
            GameObject gameObject,
            IIdentifier prefabResourceId);
    }
}