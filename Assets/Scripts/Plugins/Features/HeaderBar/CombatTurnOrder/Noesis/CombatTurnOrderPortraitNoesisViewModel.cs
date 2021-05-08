#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
using Color = Noesis.Color;
#else
using System.Windows.Media;
#endif

using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.HeaderBar.CombatTurnOrder.Noesis
{
    public sealed class CombatTurnOrderPortraitNoesisViewModel : ICombatTurnOrderPortraitNoesisViewModel
    {
        public CombatTurnOrderPortraitNoesisViewModel(
            ImageSource icon,
            IIdentifier actorIdentifier,
            Brush borderBrush,
            Brush backgroundBrush,
            string actorName)
        {
            Icon = icon;
            ActorIdentifier = actorIdentifier;
            BorderBrush = borderBrush;
            BackgroundBrush = backgroundBrush;
            ActorName = actorName;
        }

        public ImageSource Icon { get; }

        public IIdentifier ActorIdentifier { get; }

        public Brush BorderBrush { get; }

        public Brush BackgroundBrush { get; }

        public string ActorName { get; }
    }
}