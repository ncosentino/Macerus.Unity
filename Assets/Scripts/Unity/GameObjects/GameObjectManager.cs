using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Unity.GameObjects
{
    using Object = UnityEngine.Object;

    public sealed class GameObjectManager : IGameObjectManager
    {
        private readonly IObjectDestroyer _objectDestroyer;

        public GameObjectManager(IObjectDestroyer objectDestroyer)
        {
            _objectDestroyer = objectDestroyer;
        }

        public IEnumerable<GameObject> FindAll()
        {
            return UnityEngine
                .Resources
                .FindObjectsOfTypeAll(typeof(GameObject))
                .Cast<GameObject>();
        }

        public IEnumerable<GameObject> FindAll(Predicate<GameObject> filter)
        {
            return FindAll().Where(x => filter(x));
        }

        public IEnumerable<GameObject> FindAllDontDestroyOnLoad()
        {
            var killMe = new GameObject("sacrifice");

            Object.DontDestroyOnLoad(killMe);
            try
            {
                return killMe
                    .scene
                    .GetRootGameObjects()
                    .Except(new[] { killMe });
            }
            finally 
            {
                _objectDestroyer.Destroy(killMe);
            }
        }
    }
}
