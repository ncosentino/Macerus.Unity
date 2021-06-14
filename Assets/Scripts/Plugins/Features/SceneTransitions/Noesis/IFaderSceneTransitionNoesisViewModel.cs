#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
#endif

using System.ComponentModel;

namespace Assets.Scripts.Plugins.Features.SceneTransitions.Noesis
{
    public interface IFaderSceneTransitionNoesisViewModel : INotifyPropertyChanged
    {
        double Opacity { get; }

        Visibility Visibility { get; }
    }
}