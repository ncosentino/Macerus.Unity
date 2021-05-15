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
        private readonly Lazy<IReadOnlyCollection<IDiscoverableBehaviorConverter>> _converters;

        public BehaviorConverterFacade(Lazy<IEnumerable<IDiscoverableBehaviorConverter>> converters)
        {
            _converters = new Lazy<IReadOnlyCollection<IDiscoverableBehaviorConverter>>(() => converters.Value.ToArray());
        }

        private IReadOnlyCollection<IDiscoverableBehaviorConverter> Converters => _converters.Value;

        public IBehavior Convert(Component component)
        {
            var converter = Converters.FirstOrDefault(x => x.CanConvert(component));
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
            var converter = Converters.FirstOrDefault(x => x.CanConvert(behavior));
            if (converter == null)
            {
                return null;
            }

            var converted = converter.Convert(
                target,
                behavior);
            return converted;
        }
    }
}
