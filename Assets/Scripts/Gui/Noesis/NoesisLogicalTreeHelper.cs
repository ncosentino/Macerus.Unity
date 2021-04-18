#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
#endif

using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Gui.Noesis
{
    public static class NoesisLogicalTreeHelper
    {
        public static FrameworkElement FindChildWithName(
            DependencyObject debObj,
            string name) => FindChildWithName<FrameworkElement>(
                debObj,
                name);

        public static T FindChildWithName<T>(
            DependencyObject debObj,
            string name)
            where T : FrameworkElement
            => FindChildren(debObj)
            .TakeTypes<T>()
            .FirstOrDefault(x => x.Name == name);

        public static IEnumerable<DependencyObject> FindChildren(DependencyObject depObj)
            => FindChildren<DependencyObject>(depObj);

        public static IEnumerable<T> FindChildren<T>(DependencyObject depObj) 
            where T : DependencyObject
        {
            if (depObj == null)
            {
                yield break;
            }

            foreach (var child in LogicalTreeHelper.GetChildren(depObj))
            {
                if (child != null && child is T)
                {
                    yield return (T)child;
                }

                if (child is DependencyObject)
                {
                    foreach (T childOfChild in FindChildren<T>((DependencyObject)child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
