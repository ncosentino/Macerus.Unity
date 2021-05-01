using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public interface IMacerusToUnityWorldLocationSynchronizer
    {
        void SynchronizeMacerusToUnityWorldLocation(
            GameObject unityGameObject,
            double worldLocationX,
            double worldLocationY);

        void SynchronizeMacerusToUnitySize(
            GameObject unityGameObject,
            double width,
            double height);
    }
}