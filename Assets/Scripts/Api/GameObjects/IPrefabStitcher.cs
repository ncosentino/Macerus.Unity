using UnityEngine;

namespace Assets.Scripts.Api.GameObjects
{
    public interface IPrefabStitcher
    {
        void Stitch(
            GameObject gameObject,
            string prefabResourceId);
    }
}