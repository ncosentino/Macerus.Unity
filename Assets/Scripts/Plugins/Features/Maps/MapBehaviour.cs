using System;
using System.Linq;

using Assets.Scripts.Plugins.Features.Maps.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Framework.Contracts;
using ProjectXyz.Game.Interface.Mapping;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public sealed class MapBehaviour :
        MonoBehaviour,
        IMapBehaviour
    {
        public IGameObjectManager GameObjectManager { get; set; }

        public IMapProvider MapProvider { get; set; }

        public IMapFormatter ExploreMapFormatter { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                MapProvider,
                $"{nameof(MapProvider)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                GameObjectManager,
                $"{nameof(GameObjectManager)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                ExploreMapFormatter,
                $"{nameof(ExploreMapFormatter)} was not set on '{gameObject}.{this}'.");

            MapProvider.MapChanged += MapProvider_MapChanged;
            GameObjectManager.Synchronized += GameObjectManager_Synchronized;

            if (MapProvider.ActiveMap != null)
            {
                RecreateMap(MapProvider.ActiveMap);
            }
        }

        private void OnDestroy()
        {
            if (MapProvider != null)
            {
                MapProvider.MapChanged -= MapProvider_MapChanged;
            }

            if (GameObjectManager != null)
            {
                GameObjectManager.Synchronized -= GameObjectManager_Synchronized;
            }
        }

        private void GameObjectManager_Synchronized(
            object sender,
            GameObjectsSynchronizedEventArgs e)
        {
            Debug.Log($"Synchronizing game objects for map '{gameObject}'...");
            ExploreMapFormatter.RemoveGameObjects(
                gameObject,
                e.Removed
                 .Select(x => x.Behaviors.Get<IIdentifierBehavior>().First())
                 .Select(x => x.Id));
            ExploreMapFormatter.AddGameObjects(
                gameObject,
                e.Added);
            Debug.Log($"Synchronized game objects for map '{gameObject}'.");
        }

        private void MapProvider_MapChanged(
            object sender,
            EventArgs e) => RecreateMap(MapProvider.ActiveMap);

        private void RecreateMap(IMap map) => ExploreMapFormatter.FormatMap(gameObject, map);
    }
}