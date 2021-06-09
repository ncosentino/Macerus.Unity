#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
#endif

using System.ComponentModel;

namespace Assets.Scripts.Plugins.Features.Inventory.Crafting.Noesis
{
    public interface ICraftingWindowNoesisViewModel : INotifyPropertyChanged
    {
        Visibility Visibility { get; }

        bool IsLeftDocked { get; }
    }
}