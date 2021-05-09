using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.Scripts.Unity.GameObjects
{
    public static class GameObjectExtensionMethods
    {
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

        public static TComponent GetRequiredComponentInChild<TComponent>(
            this GameObject gameObject,
            string name) => GetRequiredComponentInChild<TComponent>(
                gameObject,
                name,
                true);

        public static TComponent GetRequiredComponentInChild<TComponent>(
            this GameObject gameObject,
            string name,
            bool immediateChildrenOnly)
        {
            var matchingChildGameObject = gameObject
                .GetChildGameObjects(immediateChildrenOnly)
                .SingleOrDefault(x => x.name == name);
            if (matchingChildGameObject == null)
            {
                throw new InvalidOperationException(
                    $"Could not get component of type '{typeof(TComponent)}' " +
                    $"from child game object of '{gameObject}'. There is no " +
                    $"child with the name '{name}'.");
            }

            var matchingComponent = matchingChildGameObject.GetRequiredComponent<TComponent>();
            return matchingComponent;
        }

        public static IEnumerable<GameObject> GetChildGameObjects(
            this GameObject gameObject) => GetChildGameObjects(
                gameObject,
                true);

        public static IEnumerable<GameObject> GetChildGameObjects(
            this GameObject gameObject,
            bool immediateChildrenOnly)
        {
            if (immediateChildrenOnly)
            {
                foreach (var child in gameObject
                    .transform
                    .GetChildGameObjects())
                {
                    yield return child;
                }

                yield break;
            }

            var queue = new Queue<GameObject>();
            queue.Enqueue(gameObject);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach (var child in current
                    .transform
                    .GetChildGameObjects())
                {
                    yield return child;
                    queue.Enqueue(child);
                }
            }
        }

        public static void RemoveComponents<TComponent>(this GameObject gameObject)
        {
            foreach (var component in gameObject.GetComponents<TComponent>())
            {
                UnityEngine.Object.Destroy(component as UnityEngine.Object);
            }
        }
    }
}
