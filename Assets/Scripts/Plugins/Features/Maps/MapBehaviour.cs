using System;
using System.Linq;

using Assets.Scripts.Plugins.Features.Maps.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public sealed class MapBehaviour :
        MonoBehaviour,
        IMapBehaviour
    {
        public IReadOnlyMapGameObjectManager MapGameObjectManager { get; set; }

        public IMapProvider MapProvider { get; set; }

        public IMapFormatter ExploreMapFormatter { get; set; }

        public IWeatherManager WeatherManager { get; set; }

        public IWeatherTableRepositoryFacade WeatherTableRepositoryFacade { get; set; }

        public IFilterContextFactory FilterContextFactory { get; set; }

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, MapProvider, nameof(MapProvider));
            UnityContracts.RequiresNotNull(this, MapGameObjectManager, nameof(MapGameObjectManager));
            UnityContracts.RequiresNotNull(this, ExploreMapFormatter, nameof(ExploreMapFormatter));
            UnityContracts.RequiresNotNull(this, WeatherManager, nameof(WeatherManager));
            UnityContracts.RequiresNotNull(this, WeatherTableRepositoryFacade, nameof(WeatherTableRepositoryFacade));

            MapProvider.MapChanged += MapProvider_MapChanged;
            MapGameObjectManager.Synchronized += GameObjectManager_Synchronized;

            if (MapProvider.ActiveMap != null)
            {
                SwitchMap(MapProvider.ActiveMap);
            }
        }

        private void OnDestroy()
        {
            if (MapProvider != null)
            {
                MapProvider.MapChanged -= MapProvider_MapChanged;
            }

            if (MapGameObjectManager != null)
            {
                MapGameObjectManager.Synchronized -= GameObjectManager_Synchronized;
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
            EventArgs e) => SwitchMap(MapProvider.ActiveMap);

        private void SwitchMap(IMap map)
        {
            ExploreMapFormatter.FormatMap(gameObject, map);

            var weatherTableId = map
                .Get<IMapWeatherTableBehavior>()
                .SingleOrDefault()
                ?.WeatherTableId;
            if (weatherTableId == null)
            {
                WeatherManager.WeatherTable = null;
            }
            else
            {
                var filterContext = FilterContextFactory.CreateFilterContextForSingle(
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new IdentifierFilterAttributeValue(weatherTableId),
                        true));
                WeatherManager.WeatherTable = WeatherTableRepositoryFacade
                    .GetWeatherTables(filterContext)
                    ?.FirstOrDefault();
            }
        }
    }
}