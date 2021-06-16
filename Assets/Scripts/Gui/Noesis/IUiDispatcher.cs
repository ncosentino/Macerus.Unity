#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
#endif

using System;
using System.Threading.Tasks;

namespace Assets.Scripts.Gui.Noesis
{
    public interface IUiDispatcher
    {
        void RunOnMainThread(Action action);
    }
}
