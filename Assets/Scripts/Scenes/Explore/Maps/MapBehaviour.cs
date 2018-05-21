using System;
using ProjectXyz.Game.Interface.Mapping;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Maps
{
    public sealed class MapBehaviour : MonoBehaviour
    {
        public IMapProvider MapProvider { get; set; }

        public IExploreMapFormatter ExploreMapFormatter { get; set; }

        private void Start()
        {
            if (MapProvider == null)
            {
                throw new InvalidOperationException($"'{nameof(MapProvider)}' was not set.");
            }

            if (ExploreMapFormatter == null)
            {
                throw new InvalidOperationException($"'{nameof(ExploreMapFormatter)}' was not set.");
            }

            MapProvider.MapChanged += MapProvider_MapChanged;

            if (MapProvider.ActiveMap != null)
            {
                RecreateMap(MapProvider.ActiveMap);
            }
        }

        private void MapProvider_MapChanged(object sender, EventArgs e) => RecreateMap(MapProvider.ActiveMap);

        private void RecreateMap(IMap map)
        {
            ExploreMapFormatter.FormatMap(gameObject, map);
        }
    }
}