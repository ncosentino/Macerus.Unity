using System;
using System.Collections;

using Assets.Scripts.Unity.Threading;

using UnityEngine;

namespace Assets.Scripts.Behaviours.Threading
{
    public sealed class CoroutineRunnerBehaviour :
        MonoBehaviour,
        ICoroutineRunner
    {
        private static CoroutineRunnerBehaviour _instance;

        public static ICoroutineRunner Instance => _instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (_instance == null)
            {
                _instance = new GameObject("CoroutineRunnerBehaviour").AddComponent<CoroutineRunnerBehaviour>();
                DontDestroyOnLoad(_instance.gameObject);
            }
        }

        void ICoroutineRunner.StartCoroutine(
            IEnumerator coroutine,
            Action<Exception> errorCallback)
        {
            var wrappedForErrors = RunThrowingIterator(
                coroutine,
                errorCallback);
            StartCoroutine(wrappedForErrors);
        }

        public static IEnumerator RunThrowingIterator(
            IEnumerator enumerator,
            Action<Exception> errorCallback)
        {
            while (true)
            {
                object current;
                try
                {
                    if (enumerator.MoveNext() == false)
                    {
                        break;
                    }
                    current = enumerator.Current;
                }
                catch (Exception ex)
                {
                    if (errorCallback != null)
                    {
                        errorCallback.Invoke(ex);
                        yield break;
                    }

                    throw;
                }

                yield return current;
            }
        }
    }
}
