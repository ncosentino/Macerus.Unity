using Macerus.Api.Behaviors;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public interface IMacerusToUnityWorldLocationSynchronizer
    {
        void SynchronizeMacerusToUnityWorldLocation(
            GameObject unityGameObject,
            IReadOnlyWorldLocationBehavior worldLocationBehavior);
    }
}