﻿using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Interface.Mapping;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Maps
{
    public interface IExploreMapFormatter
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