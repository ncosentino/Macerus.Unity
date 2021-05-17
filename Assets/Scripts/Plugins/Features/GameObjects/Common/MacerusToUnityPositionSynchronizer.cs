using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public sealed class MacerusToUnityPositionSynchronizer : IMacerusToUnityPositionSynchronizer
    {
        public void SynchronizeMacerusToUnityPosition(
            GameObject unityGameObject,
            double positionX,
            double positionY)
        {
            unityGameObject.transform.position = new Vector3(
                (float)positionX,
                (float)positionY,
                unityGameObject.transform.position.z);
        }

        public void SynchronizeMacerusToUnitySize(
            GameObject unityGameObject,
            double width,
            double height)
        {
            var newLocalScale = new Vector3(
                (float)width,
                (float)height,
                unityGameObject.transform.localScale.z);
            if (unityGameObject.transform.localScale == newLocalScale)
            {
                return;
            }

            unityGameObject.transform.localScale = newLocalScale;
        }
    }
}