#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
#endif

using System.Collections.Generic;
using System.ComponentModel;

namespace Assets.Scripts.Plugins.Features.PartyBar.Noesis
{
    public interface IPartyBarNoesisViewModel : INotifyPropertyChanged
    {
        IEnumerable<IPartyBarPortraitNoesisViewModel> Portraits { get; }

        Visibility Visibility { get; }
    }
}