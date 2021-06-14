#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Media;
#endif

using System.ComponentModel;

namespace Assets.Scripts.Scenes.LoadingScreen.Gui.Noesis
{
    public interface ILoadingScreenNoesisViewModel : INotifyPropertyChanged
    {
        ImageSource BackgroundImageSource { get; }
    }
}