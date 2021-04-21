using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Assets.Scripts.Gui.Noesis.ViewModels
{
    public static class LazyNotifierMappingBuilder
    {
        public static Lazy<IReadOnlyDictionary<string, string>> BuildMapping<T>() =>
            BuildMapping<T>(Enumerable.Empty<KeyValuePair<string, string>>());

        public static Lazy<IReadOnlyDictionary<string, string>> BuildMapping<T>(IEnumerable<KeyValuePair<string, string>> additional)
        {
            return new Lazy<IReadOnlyDictionary<string, string>>(() => typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance)
                .Select(x => new KeyValuePair<string, string>(
                    ((NotifyForWrappedPropertyAttribute)x
                        .GetCustomAttributes(typeof(NotifyForWrappedPropertyAttribute))
                        .SingleOrDefault())
                    ?.PropertyName,
                    x.Name))
                .Concat(additional)
                .Where(x => !string.IsNullOrWhiteSpace(x.Key))
                .ToDictionary(x => x.Key, x => x.Value));
        }
    }
}
