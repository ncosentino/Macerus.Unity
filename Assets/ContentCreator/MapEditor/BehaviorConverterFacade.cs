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

        public IEnumerable<IBehavior> Convert(Component component)
        {
            return Converters
                .Where(x => x.CanConvert(component))
                .SelectMany(x => x.Convert(component));
        }

        public IEnumerable<Component> Convert(
            GameObject target,
            IBehavior behavior)
        {
            return Converters
                .Where(x => x.CanConvert(behavior))
                .SelectMany(x => x.Convert(target, behavior));
        }
    }
}
