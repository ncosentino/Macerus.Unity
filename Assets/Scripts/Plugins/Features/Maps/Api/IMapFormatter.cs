using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface IMapFormatter :
        IMapGridLineFormatter,
        IMapHoverSelectFormatter,
        IMapTraversableHighlighter
    {
        void FormatMap(
            IMapPrefab mapPrefab,
            IGameObject map);

        void RemoveGameObjects(
            IMapPrefab mapPrefab,
            params IIdentifier[] gameObjectIds);

        void RemoveGameObjects(
            IMapPrefab mapPrefab,
            IEnumerable<IIdentifier> gameObjectIds);

        void RemoveGameObjects(IMapPrefab mapPrefab);

        void AddGameObjects(
            IMapPrefab mapPrefab,
            params IGameObject[] gameObjects);

        void AddGameObjects(
            IMapPrefab mapPrefab,
            IEnumerable<IGameObject> gameObjects);
    }
}