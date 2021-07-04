#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Media;
#endif

using System.Windows.Input;

using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.PartyBar.Noesis
{
    public interface IPartyBarPortraitNoesisViewModel
    {
        ICommand ActivateCommand { get; }

        ImageSource Icon { get; }

        IIdentifier ActorIdentifier { get; }

        Brush BorderBrush { get; }

        Brush BackgroundBrush { get; }

        string ActorName { get; }
    }
}