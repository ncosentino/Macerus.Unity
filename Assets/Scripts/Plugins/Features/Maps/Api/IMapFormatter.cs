using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps.Api
{
    public interface IMapFormatter
    {
        void FormatMap(
            GameObject mapObject,
            IMap map);

        void RemoveGameObjects(
            GameObject mapObject,
            params IIdentifier[] gameObjectIds);

        void RemoveGameObjects(
            GameObject mapObject,
            IEnumerable<IIdentifier> gameObjectIds);

        void AddGameObjects(
            GameObject mapObject,
            params IGameObject[] gameObjects);

        void AddGameObjects(
            GameObject mapObject,
            IEnumerable<IGameObject> gameObjects);
    }
}