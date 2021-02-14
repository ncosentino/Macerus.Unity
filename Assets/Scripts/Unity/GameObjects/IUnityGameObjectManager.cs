using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Unity.GameObjects
{
    public interface IUnityGameObjectManager
    {
        IEnumerable<GameObject> FindAll();

        IEnumerable<GameObject> FindAll(Predicate<GameObject> filter);

        IEnumerable<GameObject> FindAllDontDestroyOnLoad();
    }
}