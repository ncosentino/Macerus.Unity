using System;
using System.Collections.Generic;
using Assets.Scripts.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public sealed class PrefabStitcherFacade : IPrefabStitcherFacade
    {
        private readonly Dictionary<string, PrefabStitchDelegate> _stitcherMapping;

        public PrefabStitcherFacade()
        {
            _stitcherMapping = new Dictionary<string, PrefabStitchDelegate>(StringComparer.OrdinalIgnoreCase);
        }

        public void Stitch(
            GameObject gameObject,
            string prefabResourceId)
        {
            PrefabStitchDelegate stitchCallback;
            if (!_stitcherMapping.TryGetValue(
                prefabResourceId,
                out stitchCallback))
            {
                return;
            }

            stitchCallback.Invoke(
                gameObject,
                prefabResourceId);
        }

        public void Register(
            string prefabResourceId,
            PrefabStitchDelegate stitchCallback)
        {
            _stitcherMapping.Add(prefabResourceId, stitchCallback);
        }
    }
}