#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Media;
#endif

using System.Windows.Input;

using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.HeaderBar.CombatTurnOrder.Noesis
{
    public interface ICombatTurnOrderPortraitNoesisViewModel
    {
        ICommand ActivateCommand { get; }

        ImageSource Icon { get; }

        IIdentifier ActorIdentifier { get; }

        Brush BorderBrush { get; }

        Brush BackgroundBrush { get; }

        string ActorName { get; }
    }
}