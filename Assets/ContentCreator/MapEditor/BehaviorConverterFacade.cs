using System;
using System.Collections.Generic;
using System.Linq;

using Assets.ContentCreator.MapEditor.Behaviours;

using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor
{
    public sealed class BehaviorConverterFacade : IBehaviorConverterFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableBehaviorConverter> _converters;

        public BehaviorConverterFacade(IEnumerable<IDiscoverableBehaviorConverter> converters)
        {
            _converters = converters.ToArray();
        }

        public IBehavior Convert(Component component)
        {
            var converter = _converters.FirstOrDefault(x => x.CanConvert(component));
            if (converter == null)
            {
                return null;
            }

            var converted = converter.Convert(component);
            return converted;
        }

        public Component Convert(
            GameObject target,
            IBehavior behavior)
        {
            var converter = _converters.FirstOrDefault(x => x.CanConvert(behavior));
            if (converter == null)
            {
                throw new InvalidOperationException(
                    $"No converter found for '{behavior}'.");
            }

            var converted = converter.Convert(
                target,
                behavior);
            return converted;
        }
    }
}
