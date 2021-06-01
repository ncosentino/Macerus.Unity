using System;

using Assets.Scripts.Plugins.Features.Maps.Api;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore
{
    public sealed class ScreenPointToExploreMapCellConverter : IScreenPointToMapCellConverter
    {
        private Lazy<IMapPrefab> _lazyMapPrefab;

        public ScreenPointToExploreMapCellConverter(IExploreGameRootPrefabFactory exploreGameRootPrefabFactory)
        {
            _lazyMapPrefab = new Lazy<IMapPrefab>(() =>
            {
                // pretty filthy but we assume nobody will even try this until
                // we have a map instantiated, and we assume we'll only do
                // this... once?
                return exploreGameRootPrefabFactory.GetInstance().MapPrefab;
            });
        }

        public Vector3Int Convert(Vector3 screenPoint)
        {
            var worldPoint = UnityEngine.Camera.main.ScreenToWorldPoint(screenPoint);
            
            // adjust for cell alignment
            worldPoint = new Vector3(
                worldPoint.x + 0.5f,
                worldPoint.y + 0.5f,
                worldPoint.z);
            
            var cellPosition = _lazyMapPrefab.Value.Tilemap.WorldToCell(worldPoint);
            return cellPosition;
        }
    }
}
