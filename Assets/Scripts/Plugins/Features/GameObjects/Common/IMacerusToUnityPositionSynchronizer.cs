using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public interface IMacerusToUnityPositionSynchronizer
    {
        void SynchronizeMacerusToUnityPosition(
            GameObject unityGameObject,
            double positionX,
            double positionY);

        void SynchronizeMacerusToUnitySize(
            GameObject unityGameObject,
            double width,
            double height);
    }
}