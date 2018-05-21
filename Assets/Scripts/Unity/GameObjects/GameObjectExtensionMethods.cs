using System;
using UnityEngine;

namespace Assets.Scripts.Unity.GameObjects
{
    public static class GameObjectExtensionMethods
    {
        #region Methods
        public static TComponent GetRequiredComponent<TComponent>(this GameObject gameObject)
        {
            var childComponent = (object)gameObject.GetComponent(typeof(TComponent));
            if (childComponent == null)
            {
                throw new InvalidOperationException(
                    $"Could not get component of type '{typeof(TComponent)}' " +
                    $"from game object '{gameObject}'.");
            }

            return (TComponent)childComponent;
        }

        public static bool HasRequiredComponent<TComponent>(this GameObject gameObject)
        {
            var childComponent = (object)gameObject.GetComponent(typeof(TComponent));
            return childComponent != null;
        }

        public static TComponent GetRequiredComponentInParent<TComponent>(this GameObject gameObject)
        {
            var childComponent = (object)gameObject.GetComponentInParent(typeof(TComponent));
            if (childComponent == null)
            {
                throw new InvalidOperationException(
                    $"Could not get component of type '{typeof(TComponent)}' " +
                    $"from parents of game object '{gameObject}'.");
            }

            return (TComponent)childComponent;
        }
        #endregion
    }
}
