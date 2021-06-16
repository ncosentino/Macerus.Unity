using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Assets.Scripts.Unity.Threading;

using UnityEngine;

namespace Assets.Scripts.Behaviours.Threading
{
    public sealed class DispatcherBehaviour : 
        MonoBehaviour,
        IDispatcher
    {
        private static DispatcherBehaviour _instance;
        private static Thread _mainThread;

        private volatile bool _queued;
        private List<Action> _backlog = new List<Action>(8);
        private List<Action> _actions = new List<Action>(8);

        public static IDispatcher Instance => _instance;

        public bool IsMainThread => Thread.CurrentThread == _mainThread;

        public void RunAsync(Action action)
        {
            ThreadPool.QueueUserWorkItem(o => action());
        }

        public void RunAsync<T>(Action<T> action, T state)
        {
            ThreadPool.QueueUserWorkItem(o => action((T)o), state);
        }

        public void RunAsync(Action<object> action, object state) => RunAsync<object>(action, state);

        public void RunOnMainThread(Action action)
        {
            if (IsMainThread)
            {
                action();
                return;
            }

            lock (_backlog)
            {
                _backlog.Add(action);
                _queued = true;
            }
        }

        public async Task RunOnMainThreadAsync(
            Action action,
            Func<bool> checkCompletedCallback)
        {
            if (IsMainThread)
            {
                action();
                return;
            }

            RunOnMainThread(action);
            await WaitForConditionAsync(checkCompletedCallback)
                .ConfigureAwait(false);
        }

        private static async Task WaitForConditionAsync(Func<bool> callback)
        {
            while (!callback())
            {
                await Task.Delay(0);
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (_instance == null)
            {
                _instance = new GameObject("Dispatcher").AddComponent<DispatcherBehaviour>();
                DontDestroyOnLoad(_instance.gameObject);

                _mainThread = Thread.CurrentThread;
            }
        }

        private void Update()
        {
            if (!_queued)
            {
                return;
            }

            lock (_backlog)
            {
                var tmp = _actions;
                _actions = _backlog;
                _backlog = tmp;
                _queued = false;
            }

            foreach (var action in _actions)
            {
                action();
            }

            _actions.Clear();
        }
    }
}
