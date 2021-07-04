#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
using Color = Noesis.Color;
#else
using System.Windows.Media;
#endif

using System;
using System.Windows.Input;

using ProjectXyz.Api.Framework;

using Assets.Scripts.Gui.Noesis;

namespace Assets.Scripts.Plugins.Features.PartyBar.Noesis
{
    public sealed class PartyBarPortraitNoesisViewModel : IPartyBarPortraitNoesisViewModel
    {
        public PartyBarPortraitNoesisViewModel(
            ImageSource icon,
            IIdentifier actorIdentifier,
            Brush borderBrush,
            Brush backgroundBrush,
            string actorName,
            Action activateCallback)
        {
            Icon = icon;
            ActorIdentifier = actorIdentifier;
            BorderBrush = borderBrush;
            BackgroundBrush = backgroundBrush;
            ActorName = actorName;
            ActivateCommand = new DelegateCommand(_ => activateCallback());
        }

        public ICommand ActivateCommand { get; }

        public ImageSource Icon { get; }

        public IIdentifier ActorIdentifier { get; }

        public Brush BorderBrush { get; }

        public Brush BackgroundBrush { get; }

        public string ActorName { get; }
    }
}