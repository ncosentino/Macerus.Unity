﻿using System;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts // shortened namespace for ease of ue
{
    public static class GameObjectExtensionMethods
    {
        public static bool IsPlayerControlled(this GameObject unityGameObject)
        {
            var playerControlled = unityGameObject.Has<IPlayerControlledBehavior>();
            return playerControlled;
        }

        public static bool Has<T>(this GameObject unityGameObject)
            where T : IBehavior
        {
            var result = unityGameObject
                .GetComponent<IHasGameObject>()
                ?.GameObject
                .Has<T>() == true;
            return result;
        }

        public static IEnumerable<T> Get<T>(this GameObject unityGameObject)
            where T : IBehavior
        {
            var results = unityGameObject
                .GetComponent<IHasGameObject>()
                .GameObject
                .Get<T>();
            return results;
        }

        public static T GetOnly<T>(this GameObject unityGameObject)
            where T : IBehavior
        {
            var gameObject = unityGameObject
                .GetComponent<IHasGameObject>()
                .GameObject;
            var result = gameObject.GetOnly<T>();
            return result;
        }
    }
}
