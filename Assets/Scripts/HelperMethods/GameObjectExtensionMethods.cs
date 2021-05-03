using System;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using Macerus.Api.Behaviors;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

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
                .GetGameObject()
                ?.Has<T>() == true;
            return result;
        }

        public static IGameObject GetGameObject(this GameObject unityGameObject)
        {
            var gameObject = unityGameObject
                .GetComponent<IHasGameObject>()
                ?.GameObject;
            return gameObject;
        }

        public static IEnumerable<T> Get<T>(this GameObject unityGameObject)
            where T : IBehavior
        {
            var results = unityGameObject
                .GetGameObject()
                ?.Get<T>() ?? Enumerable.Empty<T>();
            return results;
        }

        public static IEnumerable<T> GetInThisOrUpHierarchy<T>(this GameObject unityGameObject)
            where T : IBehavior
        {
            var currentUnityGameObject = unityGameObject;
            while (currentUnityGameObject != null)
            {
                var gameObject = unityGameObject.GetGameObject();
                foreach (var result in gameObject?.Get<T>() ?? Enumerable.Empty<T>())
                {
                    yield return result;
                }

                currentUnityGameObject = currentUnityGameObject.transform.parent?.gameObject;
            }
        }

        public static T GetOnly<T>(this GameObject unityGameObject)
            where T : IBehavior
        {
            var hasGameObjectBehaviours = unityGameObject
                .GetComponents<IHasGameObject>()
                .ToArray();
            Contract.Requires(
                hasGameObjectBehaviours.Length == 1,
                $"Expecting '{unityGameObject}' to have one " +
                $"'{typeof(IHasGameObject)}' behaviour but there were " +
                $"{hasGameObjectBehaviours.Length}.");
            var gameObject = hasGameObjectBehaviours
                .Single()
                .GameObject;
            var result = gameObject.GetOnly<T>();
            return result;
        }

        public static T FirstOrDefault<T>(this GameObject unityGameObject)
            where T : IBehavior
        {
            var result = unityGameObject
                .Get<T>()
                .FirstOrDefault();
            return result;
        }
    }
}
