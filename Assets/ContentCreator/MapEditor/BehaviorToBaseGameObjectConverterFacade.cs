using System;
using System.Collections.Generic;
using System.Linq;

using Assets.ContentCreator.MapEditor.Behaviours;

using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor
{
    public sealed class BehaviorToBaseGameObjectConverterFacade : IBehaviorToBaseGameObjectConverterFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableBehaviorToBaseGameObjectConverter>> _converters;

        public BehaviorToBaseGameObjectConverterFacade(Lazy<IEnumerable<IDiscoverableBehaviorToBaseGameObjectConverter>> converters)
        {
            _converters = new Lazy<IReadOnlyCollection<IDiscoverableBehaviorToBaseGameObjectConverter>>(() => converters.Value.ToArray());
        }

        private IReadOnlyCollection<IDiscoverableBehaviorToBaseGameObjectConverter> Converters => _converters.Value;

        public GameObject Convert(IBehavior behavior)
        {
            var converter = Converters.FirstOrDefault(x => x.CanConvert(behavior));
            if (converter == null)
            {
                return null;
            }

            var converted = converter.Convert(behavior);
            return converted;
        }
    }
}
