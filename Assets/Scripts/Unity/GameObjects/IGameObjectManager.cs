using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Unity.GameObjects
{
    public interface IGameObjectManager
    {
        IEnumerable<GameObject> FindAll();

        IEnumerable<GameObject> FindAll(Predicate<GameObject> filter);

        IEnumerable<GameObject> FindAllDontDestroyOnLoad();
    }
}