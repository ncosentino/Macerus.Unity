#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Media;
#endif

using System;
using System.Collections.Generic;

namespace Assets.Scripts.Plugins.Features.StatusBar.Noesis
{
    public interface IStatusBarNoesisViewModel
    {
        Tuple<double, double> LeftResource { get; }

        Tuple<double, double> RightResource { get; }

        IReadOnlyCollection<Tuple<double, string, ImageSource>> Abilities { get; }
    }
}