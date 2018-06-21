using System;
using System.Linq;
using Assets.Scripts.Unity.Threading;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Interface.Mapping;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Maps
{
    public sealed class MapBehaviour : MonoBehaviour
    {
        public IGameObjectManager GameObjectManager { get; set; }

        public IMapProvider MapProvider { get; set; }

        public IExploreMapFormatter ExploreMapFormatter { get; set; }

        public IDispatcher Dispatcher { get; set; }

        private void Start()
        {
            if (MapProvider == null)
            {
                throw new InvalidOperationException($"'{nameof(MapProvider)}' was not set.");
            }

            if (GameObjectManager == null)
            {
                throw new InvalidOperationException($"'{nameof(GameObjectManager)}' was not set.");
            }

            if (ExploreMapFormatter == null)
            {
                throw new InvalidOperationException($"'{nameof(ExploreMapFormatter)}' was not set.");
            }

            if (Dispatcher == null)
            {
                throw new InvalidOperationException($"'{nameof(Dispatcher)}' was not set.");
            }

            MapProvider.MapChanged += MapProvider_MapChanged;
            GameObjectManager.Synchronized += GameObjectManager_Synchronized;

            if (MapProvider.ActiveMap != null)
            {
                RecreateMap(MapProvider.ActiveMap);
            }
        }

        private void GameObjectManager_Synchronized(
            object sender,
            GameObjectsSynchronizedEventArgs e) =>
            Dispatcher.RunOnMainThread(() =>
            {
                ExploreMapFormatter.RemoveGameObjects(
                    gameObject,
                    e.Removed
                     .Select(x => x.Behaviors.Get<IIdentifierBehavior>().First())
                     .Select(x => x.Id));
                ExploreMapFormatter.AddGameObjects(
                    gameObject,
                    e.Added);
            });

        private void MapProvider_MapChanged(
            object sender,
            EventArgs e) =>
            Dispatcher.RunOnMainThread(() =>
            {
                RecreateMap(MapProvider.ActiveMap);
            });

        private void RecreateMap(IMap map) => ExploreMapFormatter.FormatMap(gameObject, map);
    }
}