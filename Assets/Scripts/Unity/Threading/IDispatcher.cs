using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Unity.Threading
{
    public interface IDispatcher
    {
        void RunAsync(Action action);

        void RunAsync<T>(Action<T> action, T state);

        void RunAsync(Action<object> action, object state);

        void RunOnMainThread(Action action);
    }
}
