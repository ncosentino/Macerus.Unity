using System;
using System.Collections.Generic;
using Assets.Scripts.Api.GameObjects;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public sealed class PrefabStitcherFacade : IPrefabStitcherFacade
    {
        private readonly Dictionary<string, PrefabStitchDelegate> _stitcherMapping;

        public PrefabStitcherFacade(IEnumerable<IDiscoverablePrefabStitcher> prefabStitchers)
        {
            // FIXME: you'll need to consider case sensitivity after switching to identifiers
            _stitcherMapping = new Dictionary<string, PrefabStitchDelegate>(StringComparer.OrdinalIgnoreCase);

            foreach (var stitcher in prefabStitchers)
            {
                Register(stitcher.PrefabResourceId, stitcher.Stitch);
            }
        }

        public void Stitch(
            GameObject unityGameObject,
            IGameObject gameObject,
            IIdentifier prefabResourceId)
        {
            // FIXME: proper mapping of strings and identifiers
            if (!_stitcherMapping.TryGetValue(
                prefabResourceId.ToString(),
                out var stitchCallback))
            {
                return;
            }

            stitchCallback.Invoke(
                unityGameObject,
                gameObject,
                prefabResourceId);
        }

        public void Register(
            IIdentifier prefabResourceId,
            PrefabStitchDelegate stitchCallback)
        {
            // FIXME: proper mapping of strings and identifiers
            _stitcherMapping.Add(prefabResourceId.ToString(), stitchCallback);
        }
    }
}