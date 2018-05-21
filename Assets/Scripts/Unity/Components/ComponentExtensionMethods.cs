using System;
using UnityEngine;

namespace Assets.Scripts.Unity.Components
{
    public static class ComponentExtensionMethods
    {
        #region Methods
        public static TComponent GetRequiredComponent<TComponent>(this Component component)
        {
            var childComponent = (object)component.GetComponent(typeof(TComponent));
            if (childComponent == null)
            {
                throw new InvalidOperationException(
                    $"Could not get component of type '{typeof(TComponent)}' " +
                    $"from component '{component}'.");
            }

            return (TComponent)childComponent;
        }

        public static TComponent GetRequiredComponentInParent<TComponent>(this Component component)
        {
            var childComponent = (object)component.GetComponentInParent(typeof(TComponent));
            if (childComponent == null)
            {
                throw new InvalidOperationException(
                    $"Could not get component of type '{typeof(TComponent)}' " +
                    $"from parents of component '{component}'.");
            }

            return (TComponent)childComponent;
        }

        public static TComponent GetRequiredComponentInChildren<TComponent>(this Component component)
        {
            var childComponent = (object)component.GetComponentInChildren(typeof(TComponent));
            if (childComponent == null)
            {
                throw new InvalidOperationException(
                    $"Could not get component of type '{typeof(TComponent)}' " +
                    $"from children of component '{component}'.");
            }

            return (TComponent)childComponent;
        }
        #endregion
    }
}
