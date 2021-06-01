using System;

using Assets.Scripts.Behaviours.Generic;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources.Prefabs;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public sealed class TileMarkerFactory : ITileMarkerFactory
    {
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly ITimeProvider _timeProvider;
        private readonly IPrefabCreator _prefabCreator;

        public TileMarkerFactory(
            IObjectDestroyer objectDestroyer,
            ITimeProvider timeProvider,
            IPrefabCreator prefabCreator)
        {
            _objectDestroyer = objectDestroyer;
            _timeProvider = timeProvider;
            _prefabCreator = prefabCreator;
        }

        public GameObject CreateTileMarker(
            string name,
            Vector2 position,
            Color color,
            TimeSpan? duration)
        {
            var markerObject = _prefabCreator.Create<GameObject>("Mapping/Prefabs/TileMarker");
            markerObject.name = name;
            markerObject.transform.position = new Vector3(position.x, position.y, -8);

            if (duration != null)
            {
                var selfDestructBehavior = markerObject.AddComponent<SelfDestructBehaviour>();
                selfDestructBehavior.ObjectDestroyer = _objectDestroyer;
                selfDestructBehavior.TimeProvider = _timeProvider;
                selfDestructBehavior.Duration = duration.Value;
            }

            markerObject.GetRequiredComponent<SpriteRenderer>().color = color;
            return markerObject;
        }
    }
}