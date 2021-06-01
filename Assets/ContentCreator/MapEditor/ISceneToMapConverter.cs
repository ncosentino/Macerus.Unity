using System.Collections.Generic;

using Assets.Scripts.Plugins.Features.Maps.Api;

using ProjectXyz.Api.GameObjects;

namespace Assets.ContentCreator.MapEditor
{
    public interface ISceneToMapConverter
    {
        IEnumerable<IGameObject> ConvertTiles(IMapPrefab mapPrefab);

        IEnumerable<IGameObject> ConvertGameObjects(IMapPrefab mapPrefab);

        void ConvertGameObjects(
            IMapPrefab mapPrefab,
            IEnumerable<IGameObject> gameObjects);
    }
}
