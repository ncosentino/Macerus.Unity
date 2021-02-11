using System.Collections.Generic;
using Assets.Scripts.TiledNet;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Framework;

namespace Assets.Scripts.Scenes.Explore.GameObjects.TiledNet
{
    using IMacerusObjectRepository = Macerus.Api.GameObjects.IGameObjectRepositoryFacade;

    public sealed class TiledNetGameObjectRepository : IGameObjectRepository
    {
        private readonly ITiledMapLoader _tiledMapLoader;
        private readonly IMacerusObjectRepository _gameObjectRepository;

        public TiledNetGameObjectRepository(
            ITiledMapLoader tiledMapLoader,
            IMacerusObjectRepository gameObjectRepository)
        {
            _tiledMapLoader = tiledMapLoader;
            _gameObjectRepository = gameObjectRepository;
        }

        public IEnumerable<IGameObject> LoadForMap(IIdentifier mapId)
        {
            var tiledMap = _tiledMapLoader.LoadMap(mapId);

            foreach (var tiledMapObjectLayer in tiledMap.ObjectLayers)
            {
                foreach (var tiledMapObject in tiledMapObjectLayer.Objects)
                {
                    var gameObject = _gameObjectRepository.Load(
                        new StringIdentifier(tiledMapObject.Type),
                        new StringIdentifier(tiledMapObject.Name));
                    yield return gameObject;
                }
            }
        }
    }
}