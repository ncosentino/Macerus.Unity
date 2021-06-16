using System;
using System.Collections;

namespace Assets.Scripts.Unity.Threading
{
    public interface ICoroutineRunner
    {
        void StartCoroutine(
            IEnumerator coroutine,
            Action<Exception> errorCallback);
    }
}
