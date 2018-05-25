using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Unity
{
    public static class TransformExtensionMethods
    {
        public static IEnumerable<object> GetChildren(this Transform transform)
        {
            return transform.Cast<object>();
        }

        public static IEnumerable<GameObject> GetChildGameObjects(this Transform transform)
        {
            return transform
                .GetChildren()
                .Where(x => x is Transform)
                .Cast<Transform>()
                .Select(x => x.gameObject);
        }
    }
}
