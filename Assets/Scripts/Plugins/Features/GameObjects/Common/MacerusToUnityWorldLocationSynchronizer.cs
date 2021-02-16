using Macerus.Api.Behaviors;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public sealed class MacerusToUnityWorldLocationSynchronizer : IMacerusToUnityWorldLocationSynchronizer
    {
        public void SynchronizeMacerusToUnityWorldLocation(
            GameObject unityGameObject,
            IReadOnlyWorldLocationBehavior worldLocationBehavior)
        {
            unityGameObject.transform.position = new Vector3(
                (float)worldLocationBehavior.X,
                (float)worldLocationBehavior.Y,
                unityGameObject.transform.position.z);

            var newLocalScale = new Vector3(
                (float)worldLocationBehavior.Width,
                (float)worldLocationBehavior.Height,
                unityGameObject.transform.localScale.z);
            if (unityGameObject.transform.localScale == newLocalScale)
            {
                return;
            }

            unityGameObject.transform.localScale = newLocalScale;

            var spriteRenderer = unityGameObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                foreach (var boxCollider in unityGameObject.GetComponents<BoxCollider2D>())
                {
                    boxCollider.size = new Vector3(
                       spriteRenderer.bounds.size.x / unityGameObject.transform.lossyScale.x,
                       spriteRenderer.bounds.size.y / unityGameObject.transform.lossyScale.y,
                       spriteRenderer.bounds.size.z / unityGameObject.transform.lossyScale.z);
                }

                foreach (var circleCollider in unityGameObject.GetComponents<CircleCollider2D>())
                {
                    circleCollider.radius = spriteRenderer.bounds.size.x / unityGameObject.transform.lossyScale.x;
                }
            }
        }
    }
}