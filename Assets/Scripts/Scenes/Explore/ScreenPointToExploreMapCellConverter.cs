using System;

using Assets.Scripts.Plugins.Features.Maps.Api;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore
{
    public sealed class ScreenPointToExploreMapCellConverter : IScreenPointToMapCellConverter
    {
        private readonly IExploreGameRootPrefabFactory _exploreGameRootPrefabFactory;
        private IMapPrefab _cachedMapPrefab;

        public ScreenPointToExploreMapCellConverter(IExploreGameRootPrefabFactory exploreGameRootPrefabFactory)
        {
            _exploreGameRootPrefabFactory = exploreGameRootPrefabFactory;
        }

        public Vector3Int Convert(Vector3 screenPoint)
        {
            if (_cachedMapPrefab?.GameObject == null)
            {
                _cachedMapPrefab = _exploreGameRootPrefabFactory.GetInstance().MapPrefab;
            }

            var worldPoint = UnityEngine.Camera.main.ScreenToWorldPoint(screenPoint);
            
            // adjust for cell alignment
            worldPoint = new Vector3(
                worldPoint.x + 0.5f,
                worldPoint.y + 0.5f,
                worldPoint.z);
            
            var cellPosition = _cachedMapPrefab.Tilemap.WorldToCell(worldPoint);
            return cellPosition;
        }
    }
}
