#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
#endif

using System;

namespace Assets.Scripts.Plugins.Features.StatusBar.Noesis
{
    public interface IStatusBarNoesisViewModel
    {
        Tuple<double, double> LeftResource { get; }

        Tuple<double, double> RightResource { get; }
    }
}