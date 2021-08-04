#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
#endif

namespace Assets.Scripts.Plugins.Features.Gui
{
    public interface IModalWindow
    {
        Visibility Visibility { get; set; }
    }
}
