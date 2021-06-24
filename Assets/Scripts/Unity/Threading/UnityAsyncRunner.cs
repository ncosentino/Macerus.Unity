using System;
using System.Threading.Tasks;

namespace Assets.Scripts.Unity.Threading
{
    public sealed class UnityAsynRunner
    {
        private bool _running;

        public async Task RunAsync(Func<Task> callback)
        {
            if (_running)
            {
                return;
            }

            _running = true;
            callback
                .Invoke()
                .ContinueWith(prev => _running = false)
                .Forget();
            while (_running)
            {
                await Task.Yield();
            }
        }
    }
}
