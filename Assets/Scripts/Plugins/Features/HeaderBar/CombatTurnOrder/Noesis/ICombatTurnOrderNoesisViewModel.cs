#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
using System.Windows.Media;
#endif

using System.Collections.Generic;
using System.ComponentModel;

namespace Assets.Scripts.Plugins.Features.HeaderBar.CombatTurnOrder.Noesis
{
    public interface ICombatTurnOrderNoesisViewModel : INotifyPropertyChanged
    {
        IEnumerable<ICombatTurnOrderPortraitNoesisViewModel> Portraits { get; }

        Visibility Visibility { get; }
    }
}