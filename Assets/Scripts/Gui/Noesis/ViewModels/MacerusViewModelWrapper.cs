#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
#endif

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.Gui.Default;

namespace Assets.Scripts.Gui.Noesis.ViewModels
{
    public abstract class MacerusViewModelWrapper : NotifierBase
    {
        private static ConcurrentDictionary<Type, Lazy<IReadOnlyDictionary<string, string>>> _notifierMappingsByType;

        protected readonly Type _type;

        static MacerusViewModelWrapper()
        {
            _notifierMappingsByType = new ConcurrentDictionary<Type, Lazy<IReadOnlyDictionary<string, string>>>();
        }

        public MacerusViewModelWrapper()
        {
            _type = GetType();
            if (!_notifierMappingsByType.ContainsKey(_type))
            {
                _notifierMappingsByType.TryAdd(
                    _type,
                    LazyNotifierMappingBuilder.BuildMapping(_type));
            }
        }

        protected IReadOnlyDictionary<string, string> NotifierMapping => _notifierMappingsByType[_type].Value;
    }
}