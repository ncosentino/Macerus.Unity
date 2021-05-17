using System;
using System.Collections.Generic;
using System.Linq;

using Assets.ContentCreator.MapEditor.Behaviours;

using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor
{
    public sealed class GameObjectToBehaviorConverterFacade : IGameObjectToBehaviorConverterFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableGameObjectToBehaviorConverter>> _converters;

        public GameObjectToBehaviorConverterFacade(Lazy<IEnumerable<IDiscoverableGameObjectToBehaviorConverter>> converters)
        {
            _converters = new Lazy<IReadOnlyCollection<IDiscoverableGameObjectToBehaviorConverter>>(() => converters.Value.ToArray());
        }

        private IReadOnlyCollection<IDiscoverableGameObjectToBehaviorConverter> Converters => _converters.Value;

        public IEnumerable<IBehavior> Convert(GameObject unityGameObject)
        {
            return Converters
                .Where(x => x.CanConvert(unityGameObject))
                .SelectMany(x => x.Convert(unityGameObject));
        }

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
