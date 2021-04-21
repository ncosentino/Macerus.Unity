#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
#endif

using System.ComponentModel;

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    public interface IPlayerInventoryWindowNoesisViewModel : INotifyPropertyChanged
    {
        Visibility Visibility { get; }
    }
}